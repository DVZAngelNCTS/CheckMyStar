import { Injectable } from '@angular/core';
import { ActivityDalService } from '../../70_Dal/BackOffice/Activity-dal.service';
import { ActivityGetRequest } from '../../40_Requests/BackOffice/Activity-get.request';

@Injectable({
  providedIn: 'root'
})
export class ActivityBllService {
  constructor(private activityDal: ActivityDalService) {
  }

  getActivities$(numberDays: number) {
    const request = { numberDays: numberDays } as ActivityGetRequest;

    return this.activityDal.getActivities$(request);
  }
}