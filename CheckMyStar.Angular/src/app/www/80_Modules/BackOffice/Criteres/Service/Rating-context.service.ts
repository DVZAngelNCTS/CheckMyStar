import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class RatingContextService {
  rating: number | null = null;
}
