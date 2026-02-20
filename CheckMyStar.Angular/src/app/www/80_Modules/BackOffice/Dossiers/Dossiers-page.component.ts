import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { FolderBllService } from '../../../60_Bll/BackOffice/Folder-bll.service';
import { AccommodationBllService } from '../../../60_Bll/BackOffice/Accommodation-bll.service';
import { FolderModel, AccommodationModel } from '../../../20_Models/BackOffice/Folder.model';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { FolderFilterComponent } from './Filter/Folder-filter.component';
import { FolderFilter } from '../../../30_Filters/BackOffice/Folder.filter';
import { DossierFormComponent } from './Form/Dossiers-form.component';
import { AddressBllService } from '../../../60_Bll/BackOffice/Address-bll.service';
import { AddressModel } from '../../../20_Models/Common/Address.model';

@Component({
  selector: 'app-dossiers-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, TableComponent, FormsModule, ReactiveFormsModule, PopupComponent, FolderFilterComponent, DossierFormComponent],
  templateUrl: './Dossiers-page.component.html',
  styleUrl: './Dossiers-page.component.css'
})
export class DossiersPageComponent implements OnInit {
  loading = false;
  loadingSearch = false;
  loadingReset = false;
  popupVisible = false;
  popupError: string | null = null;
  folders: FolderModel[] = [];
  tableRows: FolderTableRow[] = [];
  @ViewChild(DossierFormComponent) dossierForm?: DossierFormComponent;
  isCreatingFolder = false;
  isDeletingFolder = false;
  selectedFolderIdentifier: number | null = null;

  newAccommodation: Partial<AccommodationModel> = this.buildDefaultAccommodation();
  newFolder: Partial<FolderModel> = this.buildDefaultFolder();

  columns = [
    { icon: 'bi bi-list-ol', field: 'identifier', header: 'DossiersSection.Identifier', sortable: true, filterable: true, width: '8%' },
    { icon: 'bi bi-tag', field: 'accommodationTypeIdentifier', header: 'DossiersSection.AccommodationTypeIdentifier', sortable: true, filterable: true, width: '12%' },
    { icon: 'bi bi-house', field: 'accommodationName', header: 'DossiersSection.AccommodationName', sortable: true, filterable: true, width: '12%' },
    { icon: 'bi bi-award', field: 'accommodationCurrentStar', header: 'DossiersSection.AccommodationCurrentStar', sortable: true, filterable: true, width: '8%' },
    { icon: 'bi bi-geo-alt', field: 'accommodationAddress', header: 'DossiersSection.AccommodationAddress', sortable: true, filterable: true},
    { icon: 'bi bi-person', field: 'ownerLastName', header: 'DossiersSection.OwnerLastName', sortable: true, filterable: true, width: '10%' },
    { icon: 'bi bi-person-badge', field: 'inspectorLastName', header: 'DossiersSection.InspectorLastName', sortable: true, filterable: true, width: '10%' },
    { icon: 'bi bi-info-circle', field: 'folderStatus', header: 'DossiersSection.Status', sortable: true, filterable: true, width: '10%' }
  ] as TableColumn<FolderTableRow>[];

  constructor(private folderBll: FolderBllService, private accommodationBll: AccommodationBllService, private addressBll: AddressBllService, private translate: TranslateService, private toast: ToastService) {}

  ngOnInit(): void {
    this.loadFolders();
  }

  loadFolders(isReset: boolean = false) {
    if (isReset) {
      this.loadingReset = true;
    } else {
      this.loading = true;
    }

    this.folderBll.getFolders$().subscribe({
      next: response => {
        this.folders = response.folders ?? [];
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
    const statusFilter = filter.folderStatus != null ? Number(filter.folderStatus) : null;

    if (filter.reset) {
      this.loadingReset = true;
    } else {
      this.loadingSearch = true;
    }

    if (filter.reset) {
      this.loadingReset = true;
      this.loadFolders(true);
      return;
    }

    this.loadingSearch = true;

    console.log('All filter values:', { accommodationName: filter.accommodationName, ownerLastName: filter.ownerLastName, inspectorLastName: filter.inspectorLastName, folderStatus: filter.folderStatus });

    this.tableRows = this.folders.filter(folder => {
      const accommodation = folder.accommodation;
      const owner = folder.ownerUser;
      const inspector = folder.inspectorUser;

      if (filter.accommodationName && !accommodation?.accommodationName?.toLowerCase().includes(filter.accommodationName.toLowerCase())) {
        return false;
      }
      if (filter.ownerLastName && !owner?.lastName?.toLowerCase().includes(filter.ownerLastName.toLowerCase())) {
        return false;
      }
      if (filter.inspectorLastName && !inspector?.lastName?.toLowerCase().includes(filter.inspectorLastName.toLowerCase())) {
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
    console.log('Filtered results:', this.tableRows.length);
  }

  private extractStatusId(status: unknown): number | null {
    if (!status) return null;

    if (typeof status === 'number') {
      return status;
    }

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

  openCreate() {
    this.isCreatingFolder = true;
    this.popupError = null;
    this.newAccommodation = this.buildDefaultAccommodation();
    this.newFolder = this.buildDefaultFolder();
    this.popupVisible = true;
  }

  openUpdate(_row: FolderTableRow) {
    this.toast.show(this.translate.instant('DossiersSection.ActionNotAvailable'), 'info', 4000);
  }

  openDelete(row: FolderTableRow) {
    this.isDeletingFolder = true;
    this.isCreatingFolder = false;
    this.selectedFolderIdentifier = row.identifier;
    this.popupError = null;
    this.popupVisible = true;
  }

  toggleEnabled(_row: FolderTableRow) {
    this.toast.show(this.translate.instant('DossiersSection.ActionNotAvailable'), 'info', 4000);
  }

  onPopupConfirm() {
    if (this.isCreatingFolder) {
      this.saveFolder();
    } else if (this.isDeletingFolder) {
      this.onDeleteConfirmed();
    } else {
      this.popupVisible = false;
    }
  }

  onPopupCancel() {
    this.popupVisible = false;
    this.isCreatingFolder = false;
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

  private saveFolder() {
    this.loading = true;
    this.popupError = null;

    // Step 1: Get next address identifier
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

        // Step 2: Create address
        this.addressBll.addAddress$({ address }).subscribe({
          next: () => {
            // Inject the address identifier into the accommodation
            this.newAccommodation.address = { ...this.newAccommodation.address, identifier: addressId } as any;

            // Step 3: Get next accommodation identifier
            this.accommodationBll.getNextIdentifier$().subscribe({
              next: (accommodationId) => {
                this.newAccommodation.identifier = accommodationId;

                // Step 4: Create the accommodation
                this.accommodationBll.createAccommodation$(this.newAccommodation as AccommodationModel).subscribe({
                  next: (accommodationResponse) => {
                    // Set the accommodation ID and type ID in the folder
                    this.newFolder.accommodationIdentifier = accommodationResponse.identifier;
                    this.newFolder.accommodationTypeIdentifier = accommodationResponse.accommodationType?.identifier ?? 0;

                    // Step 5: Get next folder identifier
                    this.folderBll.getNextIdentifier$().subscribe({
                      next: (folderId) => {
                        this.newFolder.identifier = folderId;

                        // Step 6: Create the folder
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
                      error: (err) => {
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
              error: (err) => {
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
      error: (err) => {
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
      inspectorUserIdentifier: 0,
      folderStatusIdentifier: 0,
      quoteIdentifier: null,
      invoiceIdentifier: null,
      appointmentIdentifier: null,
      createdDate: new Date().toISOString(),
      updatedDate: new Date().toISOString()
    } as any;
  }

  private mapToRow(folder: FolderModel): FolderTableRow {
    const accommodation = folder.accommodation;
    const owner = folder.ownerUser;
    const inspector = folder.inspectorUser;
    const address = accommodation?.address;

    return {
      identifier: folder.identifier,
      accommodationTypeIdentifier: folder.accommodationType?.identifier ?? '',
      accommodationName: accommodation?.accommodationName ?? '',
      accommodationCurrentStar: this.formatStar(accommodation?.accommodationCurrentStar),
      accommodationAddress: this.formatAddress(address),
      ownerLastName: owner?.lastName ?? '',
      inspectorLastName: inspector?.lastName ?? '',
      folderStatus: this.formatFolderStatus(folder.folderStatus),
    };
  }

  private formatAddress(address?: FolderAddressModel | null): string {
    if (!address) {
      return '';
    }

    const line1 = [address.number, address.addressLine].filter(Boolean).join(' ');
    const line2 = [address.zipCode, address.city].filter(Boolean).join(' ');
    const country = address.country?.name ?? '';

    const fullAddress = [line1, line2, country].filter(Boolean).join(', ');
    
    return fullAddress || '';
  }

  private formatStar(star: number | null | undefined): string {
    if (star == null) {
      return '';
    }

    return star === 1 ? '1 étoile' : `${star} étoiles`;
  }

  private formatFolderStatus(status: unknown): string {
    if (status == null) return '';

    let numericStatus: number | null = null;

    if (typeof status === 'object' && status !== null && 'identifier' in status) {
      const id = (status as any).identifier;
      if (typeof id === 'number') {
        numericStatus = id;
      } else if (typeof id === 'string') {
        const parsed = parseInt(id, 10);
        if (!isNaN(parsed)) numericStatus = parsed;
      }
    }

    else if (typeof status === 'number') {
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
      if (translationKey) {
        return this.translate.instant(translationKey);
      }
      return numericStatus.toString();
    }

    return '';
  }
}

interface FolderTableRow {
  identifier: number;
  accommodationTypeIdentifier: number | '';
  accommodationName: string;
  accommodationCurrentStar: string;
  accommodationAddress: string;
  ownerLastName: string;
  inspectorLastName: string;
  folderStatus: string;
}

interface FolderAddressModel {
  identifier?: number | null;
  number?: string | null;
  addressLine?: string | null;
  city?: string | null;
  zipCode?: string | null;
  region?: string | null;
  country?: {
    name?: string | null;
  } | null;
}