import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { EvaluationFormData } from "../../../../../20_Models/FrontOffice/Evaluation.models";
import { TooltipDirective } from '../../../../Components/Tooltip/Tooltip.directive';

@Component({
  selector: 'app-eval-step1',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslationModule, TooltipDirective],
  templateUrl: './Evaluation-step1.component.html',
  styleUrl: './Evaluation-step1.component.css'
})
export class EvaluationStep1Component {
  @Input() form!: EvaluationFormData;
  @Output() validated = new EventEmitter<void>();

  submitted = false;
  stars = [1, 2, 3, 4, 5];

  get isFormValid(): boolean {
    const f = this.form;
    if (!f.targetStar) return false;
    if (!f.maxCapacity || f.maxCapacity < 1) return false;
    if (f.floors === null || f.floors === undefined || isNaN(f.floors) || f.floors < 0) return false;
    if (!f.totalArea || f.totalArea < 1) return false;
    if (!f.roomCount || f.roomCount < 1) return false;
    if (!f.totalRoomsArea || f.totalRoomsArea < 1) return false;
    if (!f.smallestRoomArea || f.smallestRoomArea < 1) return false;
    if (f.smallestRoomArea > f.totalRoomsArea) return false;
    return true;
  }

  validate(): void {
    this.submitted = true;
    if (!this.isFormValid) return;
    this.validated.emit();
  }

  getStarArray(n: number): number[] {
    return Array.from({ length: n }, (_, i) => i);
  }

  isFloorsInvalid(): boolean {
    return this.submitted && (
      this.form.floors === null ||
      this.form.floors === undefined ||
      isNaN(this.form.floors as number) ||
      (this.form.floors as number) < 0
    );
  }

  isSmallestRoomInvalid(): boolean {
    if (!this.submitted) return false;
    if (!this.form.smallestRoomArea || this.form.smallestRoomArea < 1) return true;
    if (this.form.totalRoomsArea !== null && this.form.smallestRoomArea > this.form.totalRoomsArea) return true;
    return false;
  }
}
