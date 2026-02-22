import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { FolderBllService } from '../../../60_Bll/BackOffice/Folder-bll.service';
import { AccommodationBllService } from '../../../60_Bll/BackOffice/Accommodation-bll.service';
import { FolderModel, AccommodationModel } from '../../../20_Models/BackOffice/Folder.model';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { FrontFolderFilterComponent } from './Filter/Folder-filter.component';
import { FolderFilter } from '../../../30_Filters/BackOffice/Folder.filter';
import { FrontDossierFormComponent } from './Form/Front-Dossier-form.component';
import { AddressBllService } from '../../../60_Bll/BackOffice/Address-bll.service';
import { AddressModel } from '../../../20_Models/Common/Address.model';
import { AuthenticateService } from '../../../90_Services/Authenticate/Authenticate.service';

@Component({
  selector: 'app-front-dossiers-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, TableComponent, FormsModule, ReactiveFormsModule, PopupComponent, FrontFolderFilterComponent, FrontDossierFormComponent],
  templateUrl: './Dossiers-page.component.html',
  styleUrl: './Dossiers-page.component.css'
})
export class FrontDossiersPageComponent implements OnInit {
  loading = false;
  loadingSearch = false;
  loadingReset = false;
  popupVisible = false;
  popupError: string | null = null;
  folders: FolderModel[] = [];
  tableRows: FrontFolderTableRow[] = [];
  currentInspectorIdentifier: number | null = null;
  @ViewChild(FrontDossierFormComponent) dossierForm?: FrontDossierFormComponent;
  isCreatingFolder = false;
  isUpdatingFolder = false;
  isDeletingFolder = false;
  selectedFolderIdentifier: number | null = null;

  newAccommodation: Partial<AccommodationModel> = this.buildDefaultAccommodation();
  newFolder: Partial<FolderModel> = this.buildDefaultFolder();

  editAccommodation: Partial<AccommodationModel> = this.buildDefaultAccommodation();
  editFolder: Partial<FolderModel> = this.buildDefaultFolder();

  columns = [
    { icon: 'bi bi-list-ol', field: 'identifier', header: 'DossiersSection.Identifier', sortable: true, filterable: true, width: '8%' },
    { icon: 'bi bi-tag', field: 'accommodationTypeIdentifier', header: 'DossiersSection.AccommodationTypeIdentifier', sortable: true, filterable: true, width: '12%' },
    { icon: 'bi bi-house', field: 'accommodationName', header: 'DossiersSection.AccommodationName', sortable: true, filterable: true, width: '12%' },
    { icon: 'bi bi-award', field: 'accommodationCurrentStar', header: 'DossiersSection.AccommodationCurrentStar', sortable: true, filterable: true, width: '8%' },
    { icon: 'bi bi-geo-alt', field: 'accommodationAddress', header: 'DossiersSection.AccommodationAddress', sortable: true, filterable: true},
    { icon: 'bi bi-person', field: 'ownerLastName', header: 'DossiersSection.OwnerLastName', sortable: true, filterable: true, width: '10%' },
    { icon: 'bi bi-person-badge', field: 'inspectorLastName', header: 'DossiersSection.InspectorLastName', sortable: true, filterable: true, width: '10%' },
    { icon: 'bi bi-info-circle', field: 'folderStatus', header: 'DossiersSection.Status', sortable: true, filterable: true, width: '10%' }
  ] as TableColumn<FrontFolderTableRow>[];

  constructor(
    private folderBll: FolderBllService,
    private accommodationBll: AccommodationBllService,
    private addressBll: AddressBllService,
    private translate: TranslateService,
    private toast: ToastService,
    private authService: AuthenticateService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const currentUser = this.authService.getCurrentUser();
    this.currentInspectorIdentifier = currentUser?.identifier ?? null;
    this.loadFolders();
  }

  loadFolders(isReset: boolean = false) {
    if (isReset) {
      this.loadingReset = true;
    } else {
      this.loading = true;
    }

    this.folderBll.getfoldersByInspector$(this.currentInspectorIdentifier!).subscribe({
      next: response => {
        const folders = response.folders ?? [];
        this.folders = folders;
        this.tableRows = this.folders.map(folder => this.mapToRow(folder));
        if (isReset) {
          this.loadingReset = false;
        } else {
          this.loading = false;
        }
      },
      error: err => {
        if (isReset) {
          this.loadingReset = false;
        } else {
          this.loading = false;
        }
        console.error(err);
        this.toast.show(this.translate.instant('CommonSection.UnknownError'), 'error', 5000);
      }
    });
  }

  onFilter(filter: FolderFilter) {
    if (filter.reset) {
      this.loadFolders(true);
      return;
    }

    this.loadingSearch = true;

    const statusFilter = filter.folderStatus != null ? Number(filter.folderStatus) : null;

    this.tableRows = this.folders.filter(folder => {
      const accommodation = folder.accommodation;
      const owner = folder.ownerUser;

      if (filter.accommodationName && !accommodation?.accommodationName?.toLowerCase().includes(filter.accommodationName.toLowerCase())) {
        return false;
      }
      if (filter.ownerLastName && !owner?.lastName?.toLowerCase().includes(filter.ownerLastName.toLowerCase())) {
        return false;
      }
      if (statusFilter && statusFilter > 0) {
        const folderStatusId = this.extractStatusId(folder.folderStatus);
        if (folderStatusId !== statusFilter) {
          return false;
        }
      }

      return true;
    }).map(folder => this.mapToRow(folder));

    this.loadingSearch = false;
  }

  openCreate() {
    this.isCreatingFolder = true;
    this.popupError = null;
    this.newAccommodation = this.buildDefaultAccommodation();
    this.newFolder = this.buildDefaultFolder();
    // Pré-remplir l'inspecteur avec l'inspecteur connecté
    this.newFolder.inspectorUserIdentifier = this.currentInspectorIdentifier ?? 0;
    this.popupVisible = true;
  }

  openUpdate(row: FrontFolderTableRow) {
    const fullFolder = this.folders.find(f => f.identifier === row.identifier);
    if (!fullFolder) {
      this.toast.show(this.translate.instant('CommonSection.UnknownError'), 'error', 4000);
      return;
    }
    const acc = fullFolder.accommodation;
    this.editAccommodation = {
      identifier: acc?.identifier ?? 0,
      accommodationName: acc?.accommodationName ?? '',
      accommodationPhone: acc?.accommodationPhone ?? '',
      accommodationType: { identifier: acc?.accommodationType?.identifier || fullFolder.accommodationTypeIdentifier || fullFolder.accommodationType?.identifier || 0 },
      accommodationCurrentStar: acc?.accommodationCurrentStar ?? 0,
      address: {
        identifier: acc?.address?.identifier ?? 0,
        number: acc?.address?.number ?? '',
        addressLine: acc?.address?.addressLine ?? '',
        city: acc?.address?.city ?? '',
        zipCode: acc?.address?.zipCode ?? '',
        region: acc?.address?.region ?? '',
        country: {
          identifier: acc?.address?.country?.identifier ?? 0,
          code: acc?.address?.country?.code ?? '',
          name: acc?.address?.country?.name ?? ''
        }
      },
      isActive: acc?.isActive ?? true,
      createdDate: acc?.createdDate ?? new Date().toISOString(),
      updatedDate: new Date().toISOString()
    } as any;
    this.editFolder = {
      identifier: fullFolder.identifier,
      accommodationTypeIdentifier: fullFolder.accommodationType?.identifier ?? fullFolder.accommodationTypeIdentifier ?? 0,
      accommodationIdentifier: acc?.identifier ?? fullFolder.accommodationIdentifier ?? 0,
      ownerUserIdentifier: fullFolder.ownerUser?.identifier ?? fullFolder.ownerUserIdentifier ?? 0,
      inspectorUserIdentifier: fullFolder.inspectorUser?.identifier ?? fullFolder.inspectorUserIdentifier ?? this.currentInspectorIdentifier ?? 0,
      folderStatusIdentifier: this.extractStatusId(fullFolder.folderStatus) ?? fullFolder.folderStatusIdentifier ?? 0,
      quoteIdentifier: (fullFolder as any).quoteIdentifier ?? null,
      invoiceIdentifier: (fullFolder as any).invoiceIdentifier ?? null,
      appointmentIdentifier: (fullFolder as any).appointmentIdentifier ?? null,
      createdDate: (fullFolder as any).createdDate ?? new Date().toISOString(),
      updatedDate: new Date().toISOString()
    } as any;
    this.isUpdatingFolder = true;
    this.isCreatingFolder = false;
    this.isDeletingFolder = false;
    this.popupError = null;
    this.popupVisible = true;
  }

  onDetail(row: FrontFolderTableRow) {
    const fullFolder = this.folders.find(f => f.identifier === row.identifier) ?? null;
    this.router.navigate(['/fronthome/dossiers', row.identifier], { state: { folder: fullFolder } });
  }

  openDelete(row: FrontFolderTableRow) {
    this.isDeletingFolder = true;
    this.isCreatingFolder = false;
    this.selectedFolderIdentifier = row.identifier;
    this.popupError = null;
    this.popupVisible = true;
  }

  toggleEnabled(_row: FrontFolderTableRow) {
    this.toast.show(this.translate.instant('DossiersSection.ActionNotAvailable'), 'info', 4000);
  }

  onPopupConfirm() {
    if (this.isCreatingFolder) {
      this.saveFolder();
    } else if (this.isUpdatingFolder) {
      this.updateFolderSave();
    } else if (this.isDeletingFolder) {
      this.onDeleteConfirmed();
    } else {
      this.popupVisible = false;
    }
  }

  onPopupCancel() {
    this.popupVisible = false;
    this.isCreatingFolder = false;
    this.isUpdatingFolder = false;
    this.isDeletingFolder = false;
    this.selectedFolderIdentifier = null;
  }

  private onDeleteConfirmed() {
    if (!this.selectedFolderIdentifier) return;

    this.loading = true;
    this.folderBll.deleteFolder$(this.selectedFolderIdentifier).subscribe({
      next: (response: any) => {
        this.loading = false;
        if (response && response.isSuccess === false) {
          this.popupError = response.message || this.translate.instant('CommonSection.UnknownError');
          return;
        }
        this.popupVisible = false;
        this.isDeletingFolder = false;
        this.selectedFolderIdentifier = null;
        this.toast.show(this.translate.instant('DossiersSection.DeleteSuccess'), 'success', 4000);
        this.loadFolders();
      },
      error: (err) => {
        this.loading = false;
        this.popupError = err?.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  private updateFolderSave() {
    this.loading = true;
    this.popupError = null;

    // Step 1: update address
    const address = {
      identifier: (this.editAccommodation.address as any)?.identifier ?? 0,
      number: this.editAccommodation.address?.number ?? '',
      addressLine: this.editAccommodation.address?.addressLine ?? '',
      city: this.editAccommodation.address?.city ?? '',
      zipCode: this.editAccommodation.address?.zipCode ?? '',
      region: this.editAccommodation.address?.region ?? '',
      country: {
        identifier: this.editAccommodation.address?.country?.identifier ?? 0,
        code: this.editAccommodation.address?.country?.code ?? '',
        name: this.editAccommodation.address?.country?.name ?? ''
      },
      createdDate: (this.editAccommodation.address as any)?.createdDate ?? new Date().toISOString(),
      updatedDate: new Date().toISOString()
    };

    this.addressBll.updateAddress$({ address }).subscribe({
      next: () => {
        // Step 2: update accommodation
        const accommodation = {
          ...this.editAccommodation,
          address,
          updatedDate: new Date().toISOString()
        } as AccommodationModel;

        this.accommodationBll.updateAccommodation$(accommodation).subscribe({
          next: () => {
            // Step 3: update folder
            this.editFolder.accommodationTypeIdentifier = this.editAccommodation.accommodationType?.identifier ?? (this.editFolder.accommodationTypeIdentifier ?? 0);
            this.editFolder.updatedDate = new Date().toISOString();

            this.folderBll.updateFolder$(this.editFolder as FolderModel).subscribe({
              next: () => {
                this.loading = false;
                this.popupVisible = false;
                this.isUpdatingFolder = false;
                this.toast.show(this.translate.instant('DossiersSection.UpdateSuccess'), 'success', 4000);
                this.loadFolders();
              },
              error: (err) => {
                this.loading = false;
                const errorMessage = err?.message || err?.error?.message || JSON.stringify(err);
                this.popupError = this.translate.instant('DossiersSection.FolderCreateError') + ': ' + errorMessage;
              }
            });
          },
          error: (err) => {
            this.loading = false;
            const errorMessage = err?.message || err?.error?.message || JSON.stringify(err);
            this.popupError = this.translate.instant('DossiersSection.AccommodationCreateError') + ': ' + errorMessage;
          }
        });
      },
      error: (err) => {
        this.loading = false;
        const errorMessage = err?.message || err?.error?.message || JSON.stringify(err);
        this.popupError = 'Erreur lors de la mise à jour de l\'adresse : ' + errorMessage;
      }
    });
  }

  private saveFolder() {
    this.loading = true;
    this.popupError = null;

    // Forcer l'inspecteur à l'inspecteur connecté
    this.newFolder.inspectorUserIdentifier = this.currentInspectorIdentifier ?? 0;

    this.addressBll.getNextIdentifier$().subscribe({
      next: (addressResponse) => {
        const addressId = addressResponse.address?.identifier ?? 0;
        const address: AddressModel = {
          identifier: addressId,
          number: this.newAccommodation.address?.number ?? '',
          addressLine: this.newAccommodation.address?.addressLine ?? '',
          city: this.newAccommodation.address?.city ?? '',
          zipCode: this.newAccommodation.address?.zipCode ?? '',
          region: this.newAccommodation.address?.region ?? '',
          country: {
            identifier: this.newAccommodation.address?.country?.identifier ?? 0,
            code: this.newAccommodation.address?.country?.code ?? '',
            name: this.newAccommodation.address?.country?.name ?? ''
          }
        };

        this.addressBll.addAddress$({ address }).subscribe({
          next: () => {
            this.newAccommodation.address = { ...this.newAccommodation.address, identifier: addressId } as any;

            this.accommodationBll.getNextIdentifier$().subscribe({
              next: (accommodationResponse) => {                
                this.newAccommodation.identifier = accommodationResponse.accommodation?.identifier ?? 0;

                this.accommodationBll.createAccommodation$(this.newAccommodation as AccommodationModel).subscribe({
                  next: (accommodationResponse) => {
                    this.newFolder.accommodationIdentifier = this.newAccommodation.identifier;
                    this.newFolder.accommodationTypeIdentifier = this.newAccommodation.accommodationType?.identifier ?? 0;

                    this.folderBll.getNextIdentifier$().subscribe({
                      next: (folder) => {
                        this.newFolder.identifier = folder.folder?.identifier ?? 0;

                        this.folderBll.createFolder$(this.newFolder as FolderModel).subscribe({
                          next: () => {
                            this.loading = false;
                            this.popupVisible = false;
                            this.isCreatingFolder = false;
                            this.toast.show(this.translate.instant('DossiersSection.CreateSuccess'), 'success', 4000);
                            this.loadFolders();
                          },
                          error: (err) => {
                            this.loading = false;
                            const errorMessage = err?.message || err?.error?.message || JSON.stringify(err);
                            this.popupError = this.translate.instant('DossiersSection.FolderCreateError') + ': ' + errorMessage;
                          }
                        });
                      },
                      error: () => {
                        this.loading = false;
                        this.popupError = 'Erreur lors de la récupération de l\'identifiant du dossier';
                      }
                    });
                  },
                  error: (err) => {
                    this.loading = false;
                    const errorMessage = err?.message || err?.error?.message || JSON.stringify(err);
                    this.popupError = this.translate.instant('DossiersSection.AccommodationCreateError') + ': ' + errorMessage;
                  }
                });
              },
              error: () => {
                this.loading = false;
                this.popupError = 'Erreur lors de la récupération de l\'identifiant de l\'hébergement';
              }
            });
          },
          error: (err) => {
            this.loading = false;
            const errorMessage = err?.message || err?.error?.message || JSON.stringify(err);
            this.popupError = 'Erreur lors de la création de l\'adresse : ' + errorMessage;
          }
        });
      },
      error: () => {
        this.loading = false;
        this.popupError = 'Erreur lors de la récupération de l\'identifiant de l\'adresse';
      }
    });
  }

  private buildDefaultAccommodation(): Partial<AccommodationModel> {
    return {
      identifier: 0,
      accommodationName: '',
      accommodationPhone: '',
      accommodationType: { identifier: 0 },
      accommodationCurrentStar: 0,
      address: {
        identifier: 0,
        number: '',
        addressLine: '',
        city: '',
        zipCode: '',
        region: '',
        country: { identifier: 0, code: '', name: '' }
      },
      isActive: true,
      createdDate: new Date().toISOString(),
      updatedDate: new Date().toISOString()
    } as any;
  }

  private buildDefaultFolder(): Partial<FolderModel> {
    return {
      identifier: 0,
      accommodationTypeIdentifier: 0,
      accommodationIdentifier: 0,
      ownerUserIdentifier: 0,
      inspectorUserIdentifier: this.currentInspectorIdentifier ?? 0,
      folderStatusIdentifier: 0,
      quoteIdentifier: null,
      invoiceIdentifier: null,
      appointmentIdentifier: null,
      createdDate: new Date().toISOString(),
      updatedDate: new Date().toISOString()
    } as any;
  }

  private extractStatusId(status: unknown): number | null {
    if (!status) return null;

    if (typeof status === 'number') return status;

    if (typeof status === 'object' && 'identifier' in status) {
      const id = (status as any).identifier;
      if (typeof id === 'number') return id;
      if (typeof id === 'string') {
        const parsed = parseInt(id, 10);
        return !isNaN(parsed) ? parsed : null;
      }
    }

    if (typeof status === 'string') {
      const parsed = parseInt(status, 10);
      return !isNaN(parsed) ? parsed : null;
    }

    return null;
  }

  private mapToRow(folder: FolderModel): FrontFolderTableRow {
    const accommodation = folder.accommodation;
    const owner = folder.ownerUser;
    const address = accommodation?.address;

    return {
      identifier: folder.identifier,
      accommodationTypeIdentifier: folder.accommodationType?.identifier ?? '',
      accommodationName: accommodation?.accommodationName ?? '',
      accommodationCurrentStar: this.formatStar(accommodation?.accommodationCurrentStar),
      accommodationAddress: this.formatAddress(address),
      ownerLastName: owner?.lastName ?? '',
      folderStatus: this.formatFolderStatus(folder.folderStatus),
    };
  }

  private formatAddress(address?: FrontFolderAddressModel | null): string {
    if (!address) return '';
    const line1 = [address.number, address.addressLine].filter(Boolean).join(' ');
    const line2 = [address.zipCode, address.city].filter(Boolean).join(' ');
    const country = address.country?.name ?? '';
    return [line1, line2, country].filter(Boolean).join(', ') || '';
  }

  private formatStar(star: number | null | undefined): string {
    if (star == null) return '';
    return star === 1 ? '1 étoile' : `${star} étoiles`;
  }

  private formatFolderStatus(status: unknown): string {
    if (status == null) return '';

    let numericStatus: number | null = null;

    if (typeof status === 'object' && status !== null && 'identifier' in status) {
      const id = (status as any).identifier;
      if (typeof id === 'number') numericStatus = id;
      else if (typeof id === 'string') {
        const parsed = parseInt(id, 10);
        if (!isNaN(parsed)) numericStatus = parsed;
      }
    } else if (typeof status === 'number') {
      numericStatus = status;
    } else if (typeof status === 'string') {
      const parsed = parseInt(status, 10);
      if (!isNaN(parsed)) numericStatus = parsed;
    }

    if (numericStatus !== null) {
      const statusMap: { [key: number]: string } = {
        1: 'FrontDossiersSection.StatusInProgress',
        2: 'FrontDossiersSection.StatusWaitingQuote',
        3: 'FrontDossiersSection.StatusWaitingPayment',
        4: 'FrontDossiersSection.StatusCompleted',
        5: 'FrontDossiersSection.StatusCancelled'
      };
      const translationKey = statusMap[numericStatus];
      if (translationKey) return this.translate.instant(translationKey);
      return numericStatus.toString();
    }

    return '';
  }
}

interface FrontFolderTableRow {
  identifier: number;
  accommodationTypeIdentifier: number | '';
  accommodationName: string;
  accommodationCurrentStar: string;
  accommodationAddress: string;
  ownerLastName: string;
  folderStatus: string;
}

interface FrontFolderAddressModel {
  identifier?: number | null;
  number?: string | null;
  addressLine?: string | null;
  city?: string | null;
  zipCode?: string | null;
  region?: string | null;
  country?: { name?: string | null } | null;
}

