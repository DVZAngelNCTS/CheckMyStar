import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddressModel } from '../../../../20_Models/Common/Address.model';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { DigitsOnlyDirective } from '../../../../10_Common/InputFilter/Digit-only'
import { CountryBllService } from '../../../../60_Bll/BackOffice/Country-bll.service';
import { CountryModel } from '../../../../20_Models/Common/Country.model';
import { AddressBllService } from '../../../../60_Bll/BackOffice/Address-bll.service';
import { AddressAutocompleteComponent } from '../../../Components/AutoCompletion/Address-autocompletion.component';
import { GeolocationAddressModel } from '../../../../20_Models/Common/GeolocationAddress.model';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule, DigitsOnlyDirective, AddressAutocompleteComponent],
  templateUrl: './Address-form.component.html'
})
export class AddressFormComponent implements OnInit, OnChanges {
  @Input() address: AddressModel | null = null;
  @Input() readonlyIdentifier: boolean = true;

  form!: FormGroup;

  countries: CountryModel[] = [];

  constructor(
    private fb: FormBuilder,
    private countryBll: CountryBllService,
    private addressBll: AddressBllService,
  ) {}

  societies: any[] = [];

  ngOnInit() {
    this.buildForm();
    this.loadCountries();

    // Création → charger les identifiants
    if (!this.address) {
      this.loadIdentifiers();
    }

    // Désactiver les champs si readonlyIdentifier = true
    if (this.readonlyIdentifier) {
      this.form.get('identifier')?.disable();
    }
  }

  /** Permet au parent de récupérer les valeurs */
  getValue(): AddressModel {
    return this.form.getRawValue() as AddressModel;
  } 

  ngOnChanges(changes: SimpleChanges) {
    if (changes['address'] && this.address) {

      if (!this.form) {
        this.buildForm();
      }

      // 1) Patch des valeurs existantes
      this.form.patchValue({
        identifier: this.address?.identifier ?? null,
        number: this.address?.number ?? '',
        addressLine: this.address?.addressLine ?? '',
        city: this.address?.city ?? '',
        zipCode: this.address?.zipCode ?? '',
        region: this.address?.region ?? '',
        country: {
        identifier: this.address?.country?.identifier ?? 0,
        name: this.address?.country?.name ?? '',
        code: this.address?.country?.code ?? ''
        }
      });

      // 2) Si l'adresse n'a pas d'identifiant → on en génère un
      const addrId = this.form.get('identifier')?.value;

      if (!addrId || addrId === 0) {
        this.addressBll.getNextIdentifier$().subscribe({
          next: a => {
            const newId = a.address?.identifier;
            if (newId && newId > 0) {
              this.form.get('identifier')?.patchValue(newId);
            }
          }
        });
      }

      // 4) Désactiver les identifiants
      if (this.readonlyIdentifier) {
        this.form.get('identifier')?.disable();
      }
    }
  }
  
  private buildForm() { 
    this.form = this.fb.group({
        identifier: [this.address?.identifier ?? null],
        number: [this.address?.number ?? ''], 
        addressLine: [this.address?.addressLine ?? ''], 
        city: [this.address?.city ?? ''], 
        zipCode: [this.address?.zipCode ?? ''], 
        region: [this.address?.region ?? ''], 
        country: this.fb.group({ 
            identifier: [this.address?.country?.identifier ?? 0]
        })  
    });
  }

  loadCountries() { 
    this.countryBll.getCountries$().subscribe({ 
      next: countries => {
        this.countries = countries.countries ?? [];

        // 👉 Sélection automatique de la France si aucun pays n'est défini
        const france = this.countries.find(c => c.code === 'FR');

        const currentCountryId = this.form.get('country.identifier')?.value;

        // Cas création OU édition sans pays
        if ((!currentCountryId || currentCountryId === 0) && france) {
          this.form.get('country')?.patchValue({
            identifier: france.identifier,
            name: france.name,
            code: france.code
          });
        }
      },
      error: err => console.error(err)
    });
  }

  loadIdentifiers() {
    this.addressBll.getNextIdentifier$().subscribe({
      next: address => this.form.get('identifier')?.patchValue(address.address?.identifier ?? 0),
      error: err => console.error(err)
    });

    this.addressBll.getNextIdentifier$().subscribe({
      next: address => this.form.get('identifier')?.patchValue(address.address?.identifier ?? 0),
      error: err => console.error(err)
    });
  }

  onCountryChange(event: any) {
    const id = Number(event.target.value); // ← cast obligatoire

    const selected = this.countries.find(c => c.identifier === id);

    if (selected) {
      this.form.get('address.country')?.patchValue({
        identifier: selected.identifier,
        name: selected.name,
        code: selected.code
      });
    }
  }

  onAddressSelected(address: GeolocationAddressModel) {
    this.form.patchValue({
      addressLine: address.street,
      number: address.number,
      city: address.city,
      zipCode: address.zipCode,
    });
  }
}