import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ActivityFilterComponent } from './Filter/Activity-filter.component';
import { TranslateService } from '@ngx-translate/core';
import { ActivityModel } from '../../../../../20_Models/BackOffice/Activity.model';
import { ActivityBllService } from '../../../../../60_Bll/BackOffice/Activity-bll.service';
import { ToastService } from '../../../../../90_Services/Toast/Toast.service';
import { TableColumn } from '../../../../../80_Modules/Components/Table/Models/TableColumn.model'
import { TableComponent } from '../../../../Components/Table/Table.component'

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslateModule, ActivityFilterComponent, TableComponent],
  templateUrl: './Activity-page.component.html',
})
export class ActivityComponent {
  loading = false;
	loadingSearch = false; 
	loadingReset = false;
  activities?: ActivityModel[] = [];
  
  columns = [
    { icon: 'bi bi-list-ol', field: 'identifier', header: 'ActivitySection.Identifier', sortable: true, filterable: true, width: '10%' },
    { icon: 'bi bi-person', field: 'lastName', header: 'UserSection.LastName', translate: true, sortable: true, filterable: true, width: '10%', 
	  pipe: (_, row) => row.user.lastName
	},
    { icon: 'bi bi-person', field: 'firstName', header: 'UserSection.FirstName', translate: true, sortable: true, filterable: true, width: '10%',
	  pipe: (_, row) => row.user.firstName
	 },
    { icon: 'bi bi-activity', field: 'description', header: 'ActivitySection.Description', translate: true, sortable: true, filterable: true, width: '40%' },
    { icon: 'bi bi-calendar', field: 'date', header: 'ActivitySection.CreatedDate', translate: true, sortable: true, filterable: true, width: '15%' },
    { icon: 'bi bi-check-circle', field: 'isSuccess', header: 'ActivitySection.IsSuccess', translate: true, sortable: true, filterable: true, width: '10%',
      pipe: (value) => this.translate.instant( value ? 'CommonSection.Yes' : 'CommonSection.No') },
    ] as TableColumn<ActivityModel>[];

  constructor(private fb: FormBuilder, private activityBll: ActivityBllService, private translate: TranslateService, private toast: ToastService) { 
	}

  ngOnInit() {
		this.loadActivities();;
	}

	loadActivities() {
		this.activityBll.getDetailActivities$().subscribe({
			next: activities => this.activities = activities.activities,
			error: err => console.error(err)
		});
	}

  onFilter(filter: any) {
		if (filter.reset)
			this.loadingReset = true;
		else
			this.loadingSearch = true;

		this.activityBll.getDetailActivities$(filter.lastName, filter.firstName, filter.description, filter.createdDate, filter.isSuccess).subscribe({
			next: activities => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				this.activities = activities.activities;
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
}