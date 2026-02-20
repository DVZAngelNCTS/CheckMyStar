import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { AccommodationModel, AccommodationTypeModel, FolderModel } from '../../../../20_Models/BackOffice/Folder.model';
import { CountryBllService } from '../../../../60_Bll/BackOffice/Country-bll.service';
import { CountryModel } from '../../../../20_Models/Common/Country.model';
import { UserBllService } from '../../../../60_Bll/BackOffice/User-bll.service';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';

export interface FrontFolderStatusOption {
  identifier: number;
  name: string;
}

@Component({
  selector: 'app-front-dossier-form',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslationModule],
  templateUrl: './Front-Dossier-form.component.html'
})
export class FrontDossierFormComponent implements OnInit {
  @Input() accommodation!: Partial<AccommodationModel>;
  @Input() folder!: Partial<FolderModel>;
  @Input() loading = false;
  /** Quand true, le champ inspecteur est en lecture seule (non modifiable) */
  @Input() lockInspector = false;

  countries: CountryModel[] = [];
  owners: UserModel[] = [];

  accommodationTypes: AccommodationTypeModel[] = [
    { identifier: 1, name: 'Appartement' },
    { identifier: 2, name: 'Maison' },
    { identifier: 3, name: 'Hôtel' },
    { identifier: 4, name: 'Meublé de tourisme' }
  ];

  starOptions = [1, 2, 3, 4, 5];

  folderStatuses: FrontFolderStatusOption[] = [
    { identifier: 1, name: 'En cours' },
    { identifier: 2, name: 'En attente de devis' },
    { identifier: 3, name: 'En attente de paiement' },
    { identifier: 4, name: 'Terminé' },
    { identifier: 5, name: 'Annulé' }
  ];

  constructor(private countryBll: CountryBllService, private userBll: UserBllService) {}

  ngOnInit(): void {
    this.countryBll.getCountries$().subscribe({
      next: response => {
        this.countries = response.countries ?? [];
      },
      error: err => console.error('Failed to load countries', err)
    });

    this.userBll.getUsers$().subscribe({
      next: response => {
        const users = (response as any).users ?? [];
        this.owners = users.filter((u: UserModel) => u.role === EnumRole.User);
      },
      error: err => console.error('Failed to load users', err)
    });
  }
}
