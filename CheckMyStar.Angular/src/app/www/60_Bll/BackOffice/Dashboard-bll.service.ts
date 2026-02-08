import { Injectable } from '@angular/core';
import { DashboardDalService } from '../../70_Dal/BackOffice/Dashboard-dal.service';
import { DashboardModel } from '../../20_Models/BackOffice/Dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardBllService {
  constructor(private dashboardDal: DashboardDalService) {
  }

  getDashboard$() {
    return this.dashboardDal.getDashboard$();
  }
}