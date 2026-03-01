import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { FolderBllService } from '../../../60_Bll/BackOffice/Folder-bll.service';
import { AccommodationBllService } from '../../../60_Bll/BackOffice/Accommodation-bll.service';
import { FolderModel } from '../../../20_Models/BackOffice/Folder.model';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { FolderFilterComponent } from './Filter/Folder-filter.component';
import { FolderFilter } from '../../../30_Filters/BackOffice/Folder.filter';
import { DossierFormComponent } from './Form/Dossiers-form.component';
import { AddressBllService } from '../../../60_Bll/BackOffice/Address-bll.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dossiers-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, TableComponent, FormsModule, ReactiveFormsModule, PopupComponent, FolderFilterComponent, DossierFormComponent],
  templateUrl: './Dossiers-page.component.html'  
})
export class DossiersPageComponent implements OnInit {
  popupVisible = false; 
	popupMode: 'create' | 'edit' | 'delete' | 'createSociety' | null = null;
	popupTitle = ''; 
	popupConfirmLabel = ''; 
	popupCancelLabel = ''; 
	popupError: string | null = null;	
	loading = false;
	loadingSearch = false; 
	loadingReset = false;
  folders: FolderModel[] = [];
  @ViewChild(DossierFormComponent) folderForm!: DossierFormComponent;
  selectedFolder: FolderModel | null = null;

  columns = [
    { icon: 'bi bi-list-ol', field: 'identifier', header: 'DossiersSection.Identifier', sortable: true, filterable: true, width: '10%' },
    { icon: 'bi bi-tag', field: 'accommodationTypeIdentifier', header: 'DossiersSection.AccommodationTypeIdentifier', sortable: true, filterable: true, width: '10%',
      pipe: (_, row) => {
        const accommodation = row.accommodation;
        if (!accommodation) return '';
        const type = accommodation.accommodationType;
        if (!type) return '';
        return type.label;}
    },
    { icon: 'bi bi-house', field: 'accommodationName', header: 'DossiersSection.AccommodationName', sortable: true, filterable: true, width: '12%',
      pipe: (_, row) => {
        const accommodation = row.accommodation;
        if (!accommodation) return '';
        return accommodation.accommodationName}
     },
    { icon: 'bi bi-award', field: 'accommodationCurrentStar', header: 'DossiersSection.AccommodationCurrentStar', sortable: true, filterable: true, width: '10%',
      pipe: (_, row) => { 
        const accommodation = row.accommodation;
        if (!accommodation) return '';
        const accommodationCurrentStar = accommodation.accommodationCurrentStar;        
        if (accommodationCurrentStar == null) return ''; 
        return accommodationCurrentStar?.toString() + ' étoile(s)' }
     },
    { icon: 'bi bi-geo-alt', field: 'accommodationAddress', header: 'DossiersSection.AccommodationAddress', sortable: true, filterable: true,
      pipe: (_, row) => { 
        const accommodation = row.accommodation;
        if (!accommodation) return '';        
        const address = accommodation.address;
        if (!address) return ''; 
        const number = address.number != null ? address.number.toString() : ''; 
        const line = address.addressLine ?? ''; 
        const zip = address.zipCode ?? ''; 
        const city = address.city ?? ''; 
        const country = address.country?.name ?? ''; 
        return `${number} ${line} ${zip} ${city} ${country}`.trim();}
    },
    { icon: 'bi bi-person', field: 'ownerLastName', header: 'DossiersSection.Owner', sortable: true, filterable: true, width: '10%',
      pipe: (_, row) => { 
        const owner = row.owner;
        if (!owner) return '';
        return owner.firstName + ' ' + owner.lastName}
     },
    { icon: 'bi bi-person-badge', field: 'inspectorLastName', header: 'DossiersSection.Inspector', sortable: true, filterable: true, width: '10%',
        pipe: (_, row) => { 
        const inspector = row.inspector;
        if (!inspector) return '';
        return inspector.firstName + ' ' + inspector.lastName}
     },
    { icon: 'bi bi-info-circle', field: 'folderStatus', header: 'DossiersSection.Status', sortable: true, filterable: true, width: '11%',
        pipe: (_, row) => { 
        const folderStatus = row.folderStatus;
        if (!folderStatus) return '';
        return folderStatus.label}
     }
  ] as TableColumn<FolderModel>[];

  constructor(private folderBll: FolderBllService, private accommodationBll: AccommodationBllService, private addressBll: AddressBllService, private translate: TranslateService, private toast: ToastService, private router: Router) {}

  ngOnInit(): void {
    this.loadFolders();
  }

  loadFolders() {
		this.folderBll.getFolders$().subscribe({
			next: response => this.folders = response.folders ?? [],
			error: err => console.error(err)
		});
	}

  onFilter(filter: FolderFilter) {
    const statusFilter = filter.folderStatus != null ? Number(filter.folderStatus) : null;

    if (filter.reset) {
      this.loadingReset = true;
    } else {
      this.loadingSearch = true;
    }

    this.folderBll.getFolders$(filter.accommodationName, filter.ownerLastName, filter.inspectorLastName, filter.folderStatus ?? undefined).subscribe({
			next: folders => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				    this.folders = folders.folders ?? [];
			},
			error: err => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				console.error(err)
			}
		});
  }

  onDetail(model: FolderModel) {
    const fullFolder = this.folders.find(f => f.identifier === model.identifier) ?? null;
    this.router.navigate(['/backoffice/dossiers', model.identifier], { state: { folder: fullFolder } });
  }

  openCreate() {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.popupMode = 'create';
		this.popupTitle = this.translate.instant('DossiersSection.Create');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
  }

  openUpdate(folder: FolderModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedFolder = folder;
		this.popupMode = 'edit';
		this.popupTitle = this.translate.instant('DossiersSection.Update');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
  }

  openDelete(folder: FolderModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedFolder = folder;
		this.popupMode = 'delete';
		this.popupTitle = this.translate.instant('DossiersSection.Delete');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
  }

  onPopupConfirm() {
   if (this.popupMode === 'create') {
			if (this.folderForm.form.invalid) {
			this.folderForm.form.markAllAsTouched();
			return; // ❗ NE PAS FERMER LA POPUP
			}
			this.onCreateConfirmed();
			return;
		}

		if (this.popupMode === 'edit') {
			if (this.folderForm.form.invalid) {
			this.folderForm.form.markAllAsTouched();
			return; // ❗ NE PAS FERMER LA POPUP
			}
			this.onEditConfirmed();
			return;
		}

		if (this.popupMode === 'delete') {
			this.onDeleteConfirmed();
			return;
		}

    // 👉 Ici seulement si tout est OK
    this.popupVisible = false;
  }

  onPopupCancel() {
    this.popupVisible = false;
  }

  onCreateConfirmed() {
    if (this.folderForm.form.invalid) {
      this.folderForm.form.markAllAsTouched();        
      return;
    }

    this.loading = true;

    const newFolder = this.folderForm.getValue();

    this.folderBll.addFolder$(newFolder).subscribe({
      next: response => {
          if (!response.isSuccess) {
            this.loading = false;
            this.popupError = response.message;
            return; // ❗ ne pas fermer la popup
          }

          this.popupError = null;
          this.loadFolders();
          this.toast.show(response.message, "success", 5000);
          this.popupVisible = false;
        },
        error: err => {
          this.loading = false;
          this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
        }
    });
  }

  onEditConfirmed() {
    if (this.folderForm.form.invalid) {
      this.folderForm.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const updatedFolder = this.folderForm.getValue();

    this.folderBll.updateFolder$(updatedFolder).subscribe({
      next: response => {
        if (!response.isSuccess) {
          this.loading = false;
          this.popupError = response.message;
          return;
        }

        this.popupError = null;
        this.loadFolders();
        this.toast.show(response.message, "success", 5000);
        this.popupVisible = false;
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }    

  onDeleteConfirmed() {
    if (!this.selectedFolder) return;

    this.loading = true;

    this.folderBll.deleteFolder$(this.selectedFolder.identifier).subscribe({
      next: response => {
        if (!response.isSuccess) {
          this.loading = false;
          this.popupError = response.message;
          return;
        }

        this.popupError = null;
        this.loadFolders();		
        this.toast.show(response.message, "success", 5000);		
        this.popupVisible = false;
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  toggleEnabled(folder: FolderModel) {
    this.loading = true;

    const updatedFolder: FolderModel = {
      ...folder,
      isActive: !folder.isActive 
    };

    this.folderBll.updateFolder$(updatedFolder).subscribe({
      next: response => {
        this.loading = false;

        if (!response.isSuccess) {
          this.toast.show(response.message, "error", 5000);	
          return;
        }

        // Mise à jour locale
        this.folders = this.folders?.map(r =>
          r.identifier === folder.identifier ? updatedFolder : r
        );

        this.toast.show(response.message, "success", 5000);	
      },
      error: err => {
        this.loading = false;
        console.error(err);
      }
    });
  }
}