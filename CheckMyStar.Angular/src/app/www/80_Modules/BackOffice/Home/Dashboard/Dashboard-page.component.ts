import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardBllService } from '../../../../60_Bll/BackOffice/Dashboard-bll.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  templateUrl: './Dashboard-page.component.html',
  styleUrl: './Dashboard-page.component.css'
})
export class DashboardComponent implements AfterViewInit {
  numberUserWeeklyTrend = 0;
  numberUserMonthlyTrend = 0;
  numberUserYearlyTrend = 0;

  percentageUserWeeklyTrend = 0;
  percentageUserMonthlyTrend = 0;
  percentageUserYearlyTrend = 0;

  totalUsers = 0;
  activeUsers = 0;
  totalRoles = 0;
  totalCriteria = 0;

  usersTrend = +12; // +12% ce mois
  activeRate = Math.round((this.activeUsers / this.totalUsers) * 100);

  recentLogs = [
    "Utilisateur 'Martin' créé",
    "Rôle 'Inspecteur' modifié",
    "Utilisateur 'Durand' désactivé",
    "Adresse mise à jour pour 'Leroy'"
  ];

  constructor(private dashboardBll: DashboardBllService) {}

  ngOnInit() {
    this.dashboardBll.getDashboard$().subscribe(d => {

      this.totalUsers = d.dashboard?.numberUsers ?? 0;
      this.activeUsers = d.dashboard?.numberUserActives ?? 0;
      this.totalRoles = d.dashboard?.numberRoles ?? 0;

      this.numberUserWeeklyTrend = d.dashboard?.numberUserWeeklyTrend ?? 0;
      this.numberUserMonthlyTrend = d.dashboard?.numberUserMonthlyTrend ?? 0;
      this.numberUserYearlyTrend = d.dashboard?.numberUserYearlyTrend ?? 0;

      this.percentageUserWeeklyTrend = d.dashboard?.percentageUserWeeklyTrend ?? 0;
      this.percentageUserMonthlyTrend = d.dashboard?.percentageUserMonthlyTrend ?? 0;
      this.percentageUserYearlyTrend = d.dashboard?.percentageUserYearlyTrend ?? 0;

      this.activeRate = d.dashboard?.percentageUserActive ?? 0;
    });
  }

  @ViewChild('usersChart') usersChart!: ElementRef<HTMLCanvasElement>;

  ngAfterViewInit() {
    this.drawUsersChart();
  }

  drawUsersChart() {
    const ctx = this.usersChart.nativeElement.getContext('2d');
    if (!ctx) return;

    const data = [50, 60, 55, 70, 80, 95, 128]; // exemple

    ctx.clearRect(0, 0, 400, 200);
    ctx.beginPath();
    ctx.strokeStyle = '#0d6efd';
    ctx.lineWidth = 3;

    data.forEach((value, index) => {
      const x = index * 50;
      const y = 120 - value * 0.8;
      index === 0 ? ctx.moveTo(x, y) : ctx.lineTo(x, y);
    });

    ctx.stroke();
  }
}
