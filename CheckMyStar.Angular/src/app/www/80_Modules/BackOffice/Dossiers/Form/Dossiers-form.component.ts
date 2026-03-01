import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { FolderModel } from '../../../../20_Models/BackOffice/Folder.model';
import { AccommodationTypeModel } from '../../../../20_Models/BackOffice/AccommodationType.model';
import { CountryBllService } from '../../../../60_Bll/BackOffice/Country-bll.service';
import { CountryModel } from '../../../../20_Models/Common/Country.model';
import { UserBllService } from '../../../../60_Bll/BackOffice/User-bll.service';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { AddressAutocompleteComponent } from '../../../Components/AutoCompletion/Address-autocompletion.component';
import { GeolocationAddressModel } from '../../../../20_Models/Common/GeolocationAddress.model'
import { FolderStatusModel } from '../../../../20_Models/BackOffice/FolderStatus.model';
import { AddressBllService } from '../../../../60_Bll/BackOffice/Address-bll.service';
import { FolderBllService } from '../../../../60_Bll/BackOffice/Folder-bll.service';
import { AccommodationBllService } from '../../../../60_Bll/BackOffice/Accommodation-bll.service';
import { EnumCivility } from '../../../../10_Common/Enumerations/EnumCivility';

@Component({
  selector: 'app-dossier-form',
  standalone: true,
  imports: [CommonModule, TranslationModule, FieldComponent, ReactiveFormsModule, AddressAutocompleteComponent],
  templateUrl: './Dossier-form.component.html'
})
export class DossierFormComponent implements OnInit {
  @Input() folder: FolderModel | null = null;
  @Input() readonlyIdentifier: boolean = true;

  form!: FormGroup;
  
  countries: CountryModel[] = [];
  owners: UserModel[] = [];
  inspectors: UserModel[] = [];

  accommodationTypes: AccommodationTypeModel[] = [
    { identifier: 1, label: 'Appartement' },
    { identifier: 2, label: 'Maison' },
    { identifier: 3, label: 'Hôtel' },
    { identifier: 4, label: 'Meublé de tourisme' }
  ];

  starOptions = [1, 2, 3, 4, 5];

  folderStatuses: FolderStatusModel[] = [
    { identifier: 1, label: 'En cours' },
    { identifier: 2, label: 'En attente de devis' },
    { identifier: 3, label: 'En attente de paiement' },
    { identifier: 4, label: 'Terminé' },
    { identifier: 5, label: 'Annulé' }
  ];

  constructor(private fb: FormBuilder, private countryBll: CountryBllService, private userBll: UserBllService, private addressBll: AddressBllService, private folderBll: FolderBllService, private accommodationBll: AccommodationBllService) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadCountries();
    this.loadUsers();
    this.loadInspectors();

    // Création → charger les identifiants
    if (!this.folder) {
      this.loadIdentifiers();
    }

    // Désactiver les champs si readonlyIdentifier = true
    if (this.readonlyIdentifier) {
      this.form.get('identifier')?.disable();
      this.form.get('accommodation.identifier')?.disable();
      this.form.get('accommodation.address.identifier')?.disable();
    }
  }

  getValue(): FolderModel {
    return this.form.getRawValue() as FolderModel;
  } 

  ngOnChanges(changes: SimpleChanges) {
    if (changes['folder'] && changes['folder'].currentValue) {

      if (!this.form) {
        this.buildForm();
      }

      // 1) Patch des valeurs existantes
      this.form.patchValue({
        identifier: this.folder?.identifier ?? '',  
        accommodation: {      
          identifier: this.folder?.accommodation?.identifier ?? '',
          accommodationName: this.folder?.accommodation?.accommodationName ?? '',
          accommodationPhone: this.folder?.accommodation?.accommodationPhone ?? '',
          accommodationType: {
            identifier: this.folder?.accommodation?.accommodationType?.identifier ?? '',
            name : this.folder?.accommodation?.accommodationType?.label ?? '',
            description: this.folder?.accommodation?.accommodationType?.description ?? ''
          },
          accommodationCurrentStar: this.folder?.accommodation?.accommodationCurrentStar ?? '',
          address: {
            identifier: this.folder?.accommodation?.address?.identifier ?? null,
            number: this.folder?.accommodation?.address?.number ?? '', 
            addressLine: this.folder?.accommodation?.address?.addressLine ?? '', 
            city: this.folder?.accommodation?.address?.city ?? '', 
            zipCode: this.folder?.accommodation?.address?.zipCode ?? '', 
            region: this.folder?.accommodation?.address?.region ?? '', 
            country: { 
              identifier: this.folder?.accommodation?.address?.country?.identifier ?? null,
              name: this.folder?.accommodation?.address?.country?.name ?? '',
              code: this.folder?.accommodation?.address?.country?.code ?? ''
            }
          },
          isActive: this.folder?.isActive ?? true  
        },
        owner: {
          identifier: this.folder?.owner?.identifier ?? null,
          civility: this.folder?.owner?.civility ?? EnumCivility.Mister,
          lastName: this.folder?.owner?.lastName ?? '',
          firstName: this.folder?.owner?.firstName ?? '',
          societyIdentifier: this.folder?.owner?.societyIdentifier ?? null,
          email: this.folder?.owner?.email ?? '',
          phone: this.folder?.owner?.phone ?? '',
          role: this.folder?.owner?.role ?? EnumRole.User,
          password: '',
          address: {
            identifier: this.folder?.owner?.address?.identifier ?? null,
            number: this.folder?.owner?.address?.number ?? '',
            addressLine: this.folder?.owner?.address?.addressLine ?? '',
            city: this.folder?.owner?.address?.city ?? '',
            zipCode: this.folder?.owner?.address?.zipCode ?? '',
            region: this.folder?.owner?.address?.region ?? '',
            country: {
              identifier: this.folder?.owner?.address?.country?.identifier ?? 0,
              name: this.folder?.owner?.address?.country?.name ?? '',
              code: this.folder?.owner?.address?.country?.code ?? ''
            }
          },
          isActive: this.folder?.owner?.isActive ?? true,
          isFirstConnection: this.folder?.owner?.isFirstConnection ?? true
        },
        inspector: {
          identifier: this.folder?.inspector?.identifier ?? null,
          civility: this.folder?.inspector?.civility ?? EnumCivility.Mister,
          lastName: this.folder?.inspector?.lastName ?? '',
          firstName: this.folder?.inspector?.firstName ?? '',
          societyIdentifier: this.folder?.inspector?.societyIdentifier ?? null,
          email: this.folder?.inspector?.email ?? '',
          phone: this.folder?.inspector?.phone ?? '',
          role: this.folder?.inspector?.role ?? EnumRole.Inspector,
          password: '',
          address: {
            identifier: this.folder?.inspector?.address?.identifier ?? null,
            number: this.folder?.inspector?.address?.number ?? '',
            addressLine: this.folder?.inspector?.address?.addressLine ?? '',
            city: this.folder?.inspector?.address?.city ?? '',
            zipCode: this.folder?.inspector?.address?.zipCode ?? '',
            region: this.folder?.inspector?.address?.region ?? '',
            country: {
              identifier: this.folder?.inspector?.address?.country?.identifier ?? 0,
              name: this.folder?.inspector?.address?.country?.name ?? '',
              code: this.folder?.inspector?.address?.country?.code ?? ''
            }
          },
          isActive: this.folder?.inspector?.isActive ?? true,
          isFirstConnection: this.folder?.inspector?.isFirstConnection ?? true          
        },
        folderStatus: {
          identifier: this.folder?.folderStatus?.identifier ?? null,
          label: this.folder?.folderStatus?.label ?? null
        },
        isActive: true
      });
    }
  }

  private buildForm() { 
    this.form = this.fb.group({
      identifier: [{ value: this.folder?.identifier ?? '', disabled: this.readonlyIdentifier }, Validators.required],
      accommodation: this.fb.group({
        identifier: [{ value: this.folder?.accommodation?.identifier ?? '', disabled: this.readonlyIdentifier }, Validators.required],     
        accommodationName: [this.folder?.accommodation?.accommodationName ?? '', Validators.required],
        accommodationPhone: [this.folder?.accommodation?.accommodationPhone ?? '', this.phoneValidator],
        accommodationType: this.fb.group({
          identifier: [this.folder?.accommodation?.accommodationType?.identifier ?? null, Validators.required]       
        }),
        accommodationCurrentStar: [this.folder?.accommodation?.accommodationCurrentStar ?? null, Validators.required],
        address: this.fb.group({
          identifier: [{ value: this.folder?.accommodation?.address?.identifier ?? null, disabled: this.readonlyIdentifier }, Validators.required],
          number: [this.folder?.accommodation?.address?.number ?? '', Validators.required], 
          addressLine: [this.folder?.accommodation?.address?.addressLine ?? '', Validators.required], 
          city: [this.folder?.accommodation?.address?.city ?? '', Validators.required], 
          zipCode: [this.folder?.accommodation?.address?.zipCode ?? '', Validators.required], 
          region: [this.folder?.accommodation?.address?.region ?? null], 
          country: this.fb.group({ 
            identifier: [this.folder?.accommodation?.address?.country?.identifier ?? null, Validators.required]
          })
        }),
        isActive: [this.folder?.accommodation?.isActive ?? true] 
      }),
      owner: this.fb.group({
        identifier: [this.folder?.owner?.identifier ?? null, Validators.required],
      }),
      inspector: this.fb.group({
        identifier: [this.folder?.inspector?.identifier ?? null, Validators.required]
      }),
      folderStatus: this.fb.group({
        identifier: [this.folder?.folderStatus?.identifier ?? null, Validators.required]
      }),
      isActive: true  
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

  loadCountries() { 
    this.countryBll.getCountries$().subscribe({ 
      next: countries => {
        this.countries = countries.countries ?? [];

        // 👉 Sélection automatique de la France si aucun pays n'est défini
        const france = this.countries.find(c => c.code === 'FR');

        const currentCountryId = this.form.get('accommodation.address.country.identifier')?.value;

        // Cas création OU édition sans pays
        if ((!currentCountryId || currentCountryId === 0) && france) {
          this.form.get('accommodation.address.country')?.patchValue({
            identifier: france.identifier,
            name: france.name,
            code: france.code
          });
        }
      },
      error: err => console.error(err)
    });
  }

  loadUsers() {
      this.userBll.getUsers$(undefined, undefined, undefined, undefined, undefined, undefined, EnumRole.User).subscribe({
      next: response => {
        this.owners = (response as any).users ?? [];
      },
      error: err => console.error('Failed to load users', err)
    });
  }

  loadInspectors() {
      this.userBll.getUsers$(undefined, undefined, undefined, undefined, undefined, undefined, EnumRole.Inspector).subscribe({
      next: response => {
        this.inspectors = (response as any).users ?? [];
      },
      error: err => console.error('Failed to load users', err)
    });
  }

  onAddressSelected(address: GeolocationAddressModel) {
    this.form.get('accommodation.address')?.patchValue({
      addressLine: address.street,
      number: address.number,
      city: address.city,
      zipCode: address.zipCode,
    });
  }

  private phoneValidator(control: any) {
    const value = control.value || '';

    if (!value) return null;

    const phoneRegex =
      /^(?:0|\+33)[1-9](?:\d{2}){4}$/;

    return phoneRegex.test(value) ? null : { invalidPhone: true };
  }

  loadIdentifiers() {
    this.addressBll.getNextIdentifier$().subscribe({
      next: address => this.form.get('accommodation.address.identifier')?.patchValue(address.address?.identifier ?? 0),
      error: err => console.error(err)
    });

    this.folderBll.getNextIdentifier$().subscribe({
      next: folder => this.form.get('identifier')?.patchValue(folder.folder?.identifier ?? 0),
      error: err => console.error(err)
    });

    this.accommodationBll.getNextIdentifier$().subscribe({
      next: accommodation => this.form.get('accommodation.identifier')?.patchValue(accommodation.accommodation?.identifier ?? 0),
      error: err => console.error(err)
    });
  }
}
