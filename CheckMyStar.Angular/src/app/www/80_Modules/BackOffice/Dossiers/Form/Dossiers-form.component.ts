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

export interface FolderStatusOption {
  identifier: number;
  name: string;
}

@Component({
  selector: 'app-dossier-form',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslationModule],
  templateUrl: './Dossier-form.component.html'
})
export class DossierFormComponent implements OnInit {
  @Input() accommodation!: Partial<AccommodationModel>;
  @Input() folder!: Partial<FolderModel>;
  @Input() loading = false;

  countries: CountryModel[] = [];
  owners: UserModel[] = [];
  inspectors: UserModel[] = [];

  accommodationTypes: AccommodationTypeModel[] = [
    { identifier: 1, name: 'Appartement' },
    { identifier: 2, name: 'Maison' },
    { identifier: 3, name: 'Hôtel' },
    { identifier: 4, name: 'Meublé de tourisme' }
  ];

  starOptions = [1, 2, 3, 4, 5];

  folderStatuses: FolderStatusOption[] = [
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
        this.inspectors = users.filter((u: UserModel) => u.role === EnumRole.Inspector);
      },
      error: err => console.error('Failed to load users', err)
    });
  }
}
