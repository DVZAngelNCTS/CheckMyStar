import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { CountryModel } from '../../../../../20_Models/Common/Country.model';

@Component({
  selector: 'app-appointment-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslationModule],
  templateUrl: './Appointment-form.component.html'
})
export class AppointmentFormComponent {
  @Input() appointmentForm!: FormGroup;
  @Input() countries: CountryModel[] = [];
}
