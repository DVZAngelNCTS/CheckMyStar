import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { DigitsOnlyDirective } from '../../../../10_Common/InputFilter/Digit-only'
import { CountryBllService } from '../../../../60_Bll/BackOffice/Country-bll.service';
import { CountryModel } from '../../../../20_Models/Common/Country.model';
import { AddressBllService } from '../../../../60_Bll/BackOffice/Address-bll.service';
import { SocietyBllService } from '../../../../60_Bll/BackOffice/Society-bll.service';
import { AddressAutocompleteComponent } from '../../../Components/AutoCompletion/Address-autocompletion.component';
import { GeolocationAddressModel } from '../../../../20_Models/Common/GeolocationAddress.model';
import { SocietyModel } from '../../../../20_Models/BackOffice/Society.model';

@Component({
  selector: 'app-society-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule, DigitsOnlyDirective, AddressAutocompleteComponent],
  templateUrl: './Society-form.component.html'
})
export class SocietyFormComponent implements OnInit, OnChanges {
  @Input() society: SocietyModel | null = null;
  @Input() readonlyIdentifier: boolean = true;
  @Output() createSociety = new EventEmitter<void>();

  form!: FormGroup;

  countries: CountryModel[] = [];

  constructor(
    private fb: FormBuilder,
    private countryBll: CountryBllService,
    private addressBll: AddressBllService,
    private societyBll: SocietyBllService
  ) {}

  ngOnInit() {
    this.buildForm();
    this.loadCountries();

    // Création → charger les identifiants
    if (!this.society) {
      this.loadIdentifiers();
    }

    // Désactiver les champs si readonlyIdentifier = true
    if (this.readonlyIdentifier) {
      this.form.get('identifier')?.disable();
      this.form.get('address.identifier')?.disable();
    }
  }

  /** Permet au parent de récupérer les valeurs */
  getValue(): SocietyModel {
    return this.form.getRawValue() as SocietyModel;
  } 

  ngOnChanges(changes: SimpleChanges) {
    if (changes['society'] && changes['society'].currentValue) {

      if (!this.form) {
        this.buildForm();
      }

      // 1) Patch des valeurs existantes
      this.form.patchValue({
        identifier: this.society?.identifier ?? 0,
        name: this.society?.name ?? '',
        email: this.society?.email ?? '',
        phone: this.society?.phone ?? '',
        address: {
          identifier: this.society?.address?.identifier ?? null,
          number: this.society?.address?.number ?? '',
          addressLine: this.society?.address?.addressLine ?? '',
          city: this.society?.address?.city ?? '',
          zipCode: this.society?.address?.zipCode ?? '',
          region: this.society?.address?.region ?? '',
          country: {
            identifier: this.society?.address?.country?.identifier ?? 0,
            name: this.society?.address?.country?.name ?? '',
            code: this.society?.address?.country?.code ?? ''
          }
        },
        isActive: this.society?.isActive ?? true,
      });

      // 2) Si l'adresse n'a pas d'identifiant → on en génère un
      const addrId = this.form.get('address.identifier')?.value;

      if (!addrId || addrId === 0) {
        this.addressBll.getNextIdentifier$().subscribe({
          next: a => {
            const newId = a.address?.identifier;
            if (newId && newId > 0) {
              this.form.get('address.identifier')?.patchValue(newId);
            }
          }
        });
      }

      // 4) Désactiver les identifiants
      if (this.readonlyIdentifier) {
        this.form.get('identifier')?.disable();
        this.form.get('address.identifier')?.disable();
      }
    }
  }
  
  private buildForm() { 
    this.form = this.fb.group({
      identifier: [this.society?.identifier ?? '', Validators.required],
      name: [this.society?.name ?? '', Validators.required],      
      email: [this.society?.email ?? '', [Validators.required, this.emailValidator]],
      phone: [this.society?.phone ?? '', this.phoneValidator],
      address: this.fb.group({ 
        identifier: [this.society?.address?.identifier ?? null],
        number: [this.society?.address?.number ?? ''], 
        addressLine: [this.society?.address?.addressLine ?? ''], 
        city: [this.society?.address?.city ?? ''], 
        zipCode: [this.society?.address?.zipCode ?? ''], 
        region: [this.society?.address?.region ?? ''], 
        country: this.fb.group({ 
          identifier: [this.society?.address?.country?.identifier ?? 0]
        })
      }),
      isActive: [this.society?.isActive ?? true]   
    });
  }

  loadCountries() { 
    this.countryBll.getCountries$().subscribe({ 
      next: countries => {
        this.countries = countries.countries ?? [];

        // 👉 Sélection automatique de la France si aucun pays n'est défini
        const france = this.countries.find(c => c.code === 'FR');

        const currentCountryId = this.form.get('address.country.identifier')?.value;

        // Cas création OU édition sans pays
        if ((!currentCountryId || currentCountryId === 0) && france) {
          this.form.get('address.country')?.patchValue({
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
      next: address => this.form.get('address.identifier')?.patchValue(address.address?.identifier ?? 0),
      error: err => console.error(err)
    });

    this.societyBll.getNextIdentifier$().subscribe({
      next: society => this.form.get('identifier')?.patchValue(society.society?.identifier ?? 0),
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

  private emailValidator(control: any) {
    const value = control.value || '';

    // Regex email robuste (RFC 5322 simplifiée)
    const emailRegex =
      /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    return emailRegex.test(value) ? null : { invalidEmail: true };
  }

  private phoneValidator(control: any) {
    const value = control.value || '';

    if (!value) return null;

    const phoneRegex =
      /^(?:0|\+33)[1-9](?:\d{2}){4}$/;

    return phoneRegex.test(value) ? null : { invalidPhone: true };
  }

  onAddressSelected(address: GeolocationAddressModel) {
    this.form.get('address')?.patchValue({
      addressLine: address.street,
      number: address.number,
      city: address.city,
      zipCode: address.zipCode,
    });
  }
}