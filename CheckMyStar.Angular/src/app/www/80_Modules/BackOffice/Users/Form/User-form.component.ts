import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { DigitsOnlyDirective } from '../../../../10_Common/InputFilter/Digit-only'
import { EnumCivility } from '../../../../10_Common/Enumerations/EnumCivility';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';
import { CountryBllService } from '../../../../60_Bll/BackOffice/Country-bll.service';
import { CountryModel } from '../../../../20_Models/Common/Country.model';
import { AddressBllService } from '../../../../60_Bll/BackOffice/Address-bll.service';
import { UserBllService } from '../../../../60_Bll/BackOffice/User-bll.service';
import { SocietyBllService } from '../../../../60_Bll/BackOffice/Society-bll.service';
import { AuthenticateService } from '../../../../90_Services/Authenticate/Authenticate.service';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule, DigitsOnlyDirective],
  templateUrl: './User-form.component.html'
})
export class UserFormComponent implements OnInit, OnChanges {
  @Input() user: UserModel | null = null;
  @Input() readonlyIdentifier: boolean = true;

  form!: FormGroup;

  EnumCivility = EnumCivility;
  EnumRole = EnumRole;

  passwordVisible = false;

  countries: CountryModel[] = [];

  constructor(
    private fb: FormBuilder,
    private countryBll: CountryBllService,
    private addressBll: AddressBllService,
    private userBll: UserBllService,
    private societyBll: SocietyBllService
  ) {}

  societies: any[] = [];

  ngOnInit() {
    this.buildForm();
    this.loadCountries();
    this.loadSocieties();

    // Création → charger les identifiants
    if (!this.user) {
      this.loadIdentifiers();
    }

    // Désactiver les champs si readonlyIdentifier = true
    if (this.readonlyIdentifier) {
      this.form.get('identifier')?.disable();
      this.form.get('address.identifier')?.disable();
    }
  }

  loadSocieties() {
    this.societyBll.getSocieties$().subscribe({
      next: (response) => {
        console.log('Sociétés reçues :', response);
        this.societies = response.societies || [];
        console.log('Tableau societies :', this.societies);
      },
      error: (err) => console.error('Erreur chargement sociétés', err)
    });
  }

  /** Permet au parent de récupérer les valeurs */
  getValue(): UserModel {
    return this.form.value as UserModel;
  } 

  ngOnChanges(changes: SimpleChanges) {
    if (changes['user'] && changes['user'].currentValue) {

      if (!this.form) {
        this.buildForm();
      }

      // 1) Patch des valeurs existantes
      this.form.patchValue({
        identifier: this.user?.identifier ?? 0,
        civility: this.user?.civility ?? EnumCivility.Mister,
        lastName: this.user?.lastName ?? '',
        firstName: this.user?.firstName ?? '',
        societyIdentifier: this.user?.societyIdentifier ?? null,
        email: this.user?.email ?? '',
        phone: this.user?.phone ?? '',
        role: this.user?.role ?? EnumRole.User,
        password: '',
        address: {
          identifier: this.user?.address?.identifier ?? null,
          number: this.user?.address?.number ?? '',
          addressLine: this.user?.address?.addressLine ?? '',
          city: this.user?.address?.city ?? '',
          zipCode: this.user?.address?.zipCode ?? '',
          region: this.user?.address?.region ?? '',
          country: {
            identifier: this.user?.address?.country?.identifier ?? 0,
            name: this.user?.address?.country?.name ?? '',
            code: this.user?.address?.country?.code ?? ''
          }
        },
        isActive: this.user?.isActive ?? true,
        isFirstConnection: this.user?.isFirstConnection ?? true
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

      // 3) Validators du mot de passe (édition)
      const pwdControl = this.form.get('password');
      pwdControl?.clearValidators();
      pwdControl?.addValidators([
        (control) => {
          if (!control.value) return null;
          return this.strongPasswordValidator(control);
        }
      ]);
      pwdControl?.updateValueAndValidity();

      // 4) Désactiver les identifiants
      if (this.readonlyIdentifier) {
        this.form.get('identifier')?.disable();
        this.form.get('address.identifier')?.disable();
      }
    }
  }
  
  private buildForm() { 
    this.form = this.fb.group({
      identifier: [this.user?.identifier ?? '', Validators.required],
      civility: [this.user?.civility ?? EnumCivility.Mister, Validators.required],      
      lastName: [this.user?.lastName ?? '', Validators.required],
      firstName: [this.user?.firstName ?? '', Validators.required],
      societyIdentifier: [this.user?.societyIdentifier ?? null],
      email: [this.user?.email ?? '', [Validators.required, this.emailValidator]],
      phone: [this.user?.phone ?? '', this.phoneValidator],
      role: [this.user?.role ?? EnumRole.User, Validators.required], 
      password: [ '', this.user ? [] : [Validators.required, this.strongPasswordValidator]],
      address: this.fb.group({ 
        identifier: [this.user?.address?.identifier ?? null],
        number: [this.user?.address?.number ?? ''], 
        addressLine: [this.user?.address?.addressLine ?? ''], 
        city: [this.user?.address?.city ?? ''], 
        zipCode: [this.user?.address?.zipCode ?? ''], 
        region: [this.user?.address?.region ?? ''], 
        country: this.fb.group({ 
          identifier: [this.user?.address?.country?.identifier ?? 0],
          name: [this.user?.address?.country?.name ?? ''],
          code: [this.user?.address?.country?.code ?? '']
        })
      }),
      isActive: [this.user?.isActive ?? true],
      isFirstConnection: [this.user?.isFirstConnection ?? true]      
    });
  }

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
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

    this.userBll.getNextIdentifier$().subscribe({
      next: user => this.form.get('identifier')?.patchValue(user.user?.identifier ?? 0),
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

  generatePassword() {
    const pwd = this.authenticateService.generatePassword();

    // 4) On remplit le champ
    this.form.get('password')?.setValue(pwd);

    // Optionnel : rendre visible après génération
    this.passwordVisible = true;
  }

  private strongPasswordValidator(control: any) {
    const value = control.value || '';

    // Au moins 12 caractères
    const lengthOK = value.length >= 12;

    // Au moins une majuscule
    const upperOK = /[A-Z]/.test(value);

    // Au moins une minuscule
    const lowerOK = /[a-z]/.test(value);

    // Au moins un chiffre
    const numberOK = /[0-9]/.test(value);

    // Au moins un symbole
    const symbolOK = /[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/.test(value);

    const valid = lengthOK && upperOK && lowerOK && numberOK && symbolOK;

    return valid ? null : { weakPassword: true };
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
}