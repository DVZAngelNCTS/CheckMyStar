import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardBllService } from '../../../../60_Bll/BackOffice/Dashboard-bll.service';
import { TranslateModule } from '@ngx-translate/core';
import { ActivityModel } from '../../../../20_Models/BackOffice/Activity.model';
import { ActivityBllService } from '../../../../60_Bll/BackOffice/Activity-bll.service';
import { UserBllService } from '../../../../60_Bll/BackOffice/User-bll.service';
import { UserEvolutionModel } from '../../../../20_Models/BackOffice/UserEvolution.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  templateUrl: './Dashboard-page.component.html',
  styleUrl: './Dashboard-page.component.css'
})
export class DashboardComponent {
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

  recentActivities: ActivityModel[] = [];
  userEvolutionData: UserEvolutionModel[] = [];

  usersTrend = +12; // +12% ce mois
  activeRate = Math.round((this.activeUsers / this.totalUsers) * 100);

  constructor(private dashboardBll: DashboardBllService, private activityBll: ActivityBllService, private userBll: UserBllService) {}

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

      this.loadActivities();
      this.loadUserEvolutions();
    });
  }

  @ViewChild('usersChart') usersChart!: ElementRef<HTMLCanvasElement>;

  drawUsersChart() {
    if (!this.usersChart) return;
    const canvas = this.usersChart.nativeElement;
    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    const labels = this.getFullMonthLabels();
    const points = this.getCumulativePoints();

    const width = canvas.width;
    const height = canvas.height;

    ctx.clearRect(0, 0, width, height);

    const marginLeft = 40;
    const marginBottom = 25;

    const max = points.length > 0 ? Math.max(...points.map(p => p.value)) : 1;
    const scaleY = (height - marginBottom - 10) / max;

    // Axe Y
    ctx.beginPath();
    ctx.strokeStyle = '#ccc';
    ctx.moveTo(marginLeft, 0);
    ctx.lineTo(marginLeft, height - marginBottom);
    ctx.stroke();

    // Graduations Y
    ctx.fillStyle = '#666';
    ctx.font = '10px Arial';

    for (let i = 0; i <= max; i++) {
      const y = (height - marginBottom) - i * scaleY;
      ctx.fillText(i.toString(), 5, y + 3);

      ctx.strokeStyle = '#eee';
      ctx.beginPath();
      ctx.moveTo(marginLeft, y);
      ctx.lineTo(width, y);
      ctx.stroke();
    }

    // Axe X
    ctx.beginPath();
    ctx.strokeStyle = '#ccc';
    ctx.moveTo(marginLeft, height - marginBottom);
    ctx.lineTo(width, height - marginBottom);
    ctx.stroke();

    // Labels X
    const stepX = (width - marginLeft - 10) / 11;

    labels.forEach((label, index) => {
      const x = marginLeft + index * stepX;
      ctx.fillText(label, x - 10, height - 5);
    });

    // Courbe (uniquement les points existants)
    ctx.beginPath();
    ctx.strokeStyle = '#0d6efd';
    ctx.lineWidth = 2;

    points.forEach((p, index) => {
      const x = marginLeft + (p.month - 1) * stepX;
      const y = (height - marginBottom) - p.value * scaleY;

      index === 0 ? ctx.moveTo(x, y) : ctx.lineTo(x, y);
    });

    ctx.stroke();
  }


  loadActivities() { 
    this.activityBll.getActivities$(7).subscribe({ 
      next: (response) => { 
        if (response.isSuccess && response.activities) { 
          this.recentActivities = response.activities; } 
        }, 
        error: (err) => console.error('Erreur chargement activités', err) 
    }); 
  }

  loadUserEvolutions() {
    this.userBll.getUserEvolutions$().subscribe({
      next: (response) => {
        if (response.isSuccess && response.evolutions) {
          this.userEvolutionData = response.evolutions;

          this.drawUsersChart();
        }
      },
      error: (err) => console.error('Erreur chargement évolutions utilisateurs', err)
    });
  }

  getCumulativePoints() {
    let cumulative = 0;
    const points: { month: number, value: number }[] = [];

    const sorted = [...this.userEvolutionData].sort((a, b) => a.month - b.month);

    sorted.forEach(e => {
      cumulative += e.total;
      points.push({ month: e.month, value: cumulative });
    });

    return points;
  }

  getFullMonthLabels() {
    return ['Jan', 'Fév', 'Mar', 'Avr', 'Mai', 'Juin', 'Juil', 'Août', 'Sep', 'Oct', 'Nov', 'Déc'];
  }

	openActivityDetails() {

	}  
}
