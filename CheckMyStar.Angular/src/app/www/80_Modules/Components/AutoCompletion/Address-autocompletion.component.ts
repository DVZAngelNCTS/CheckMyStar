import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { AddressBllService } from '../../../60_Bll/BackOffice/Address-bll.service';
import { GeolocationAddressModel } from '../../../20_Models/Common/GeolocationAddress.model';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { TranslateService } from '@ngx-translate/core';
import { FieldComponent } from '../Field/Field.component';

@Component({
  selector: 'app-address-autocomplete',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent],
  templateUrl: './Address-autocompletion.component.html',
})
export class AddressAutocompleteComponent {

  @Output() addressSelected = new EventEmitter<GeolocationAddressModel>();

  control = new FormControl('');
  suggestions: GeolocationAddressModel[] = [];

  constructor(
    private addressBll: AddressBllService,
    private toast: ToastService,
    private translate: TranslateService
  ) {
    this.control.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(value => this.addressBll.searchAddress$(value ?? ''))
      )
      .subscribe({
        next: response => {
          if (!response.isSuccess) {
            this.toast.show(response.message, "error", 5000);
            return;
          }

          this.suggestions = response.addresses ?? [];
        },
        error: err => {
          this.toast.show(
            err.error?.message || this.translate.instant('CommonSection.UnknownError'),
            "error",
            5000
          );
        }
      });
  }

  selectAddress(address: GeolocationAddressModel) {
    this.addressSelected.emit(address);
    this.control.setValue(address.label, { emitEvent: false });
    this.suggestions = [];
  }
}
