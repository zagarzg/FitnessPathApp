import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router, RouterEvent } from '@angular/router';
import { take } from 'rxjs/operators';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexStroke,
  ApexMarkers,
  ApexYAxis,
  ApexGrid,
  ApexTitleSubtitle,
  ApexLegend,
} from 'ng-apexcharts';
import { TrainingLog } from '../../models/TrainingLog';
import { ChartService } from '../../services/chart.service';

export type ChartOptions = {
  series: ApexAxisChartSeries | undefined;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  dataLabels: ApexDataLabels;
  markers: ApexMarkers;
  colors: string[];
  yaxis: ApexYAxis;
  grid: ApexGrid;
  legend: ApexLegend;
  title: ApexTitleSubtitle;
};

interface ChartData {
  value: number;
  date: Date;
}

@Component({
  selector: 'app-training-chart',
  templateUrl: './training-chart.component.html',
  styleUrls: ['./training-chart.component.scss'],
})
export class TrainingChartComponent implements OnInit {
  @ViewChild('chart') chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions | any> = {};

  @Input() chartData!: TrainingLog[];

  exercises: string[] = ['Bench', 'CGBP'];
  months: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
  years: number[] = [2021, 2022, 2023];
  selectedExercise: string = 'Bench';
  monthSelected: number = new Date().getMonth() + 1;
  yearSelected: number = new Date().getFullYear();
  timeframeSelected: 'Monthly' | 'Yearly' = 'Monthly';
  progressPercentage: number = 0;

  constructor(private _chartService: ChartService) {}

  ngOnInit() {
    this.exerciseChange(this.selectedExercise);
  }

  public exerciseChange(exerciseName: string) {
    this.selectedExercise = exerciseName;

    if (this.timeframeSelected == 'Yearly') {
      this._chartService
        .getYearlyChartDataByExerciseName(
          this.selectedExercise,
          this.yearSelected
        )
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    } else {
      this._chartService
        .getMonthlyChartDataByExerciseName(
          this.selectedExercise,
          this.monthSelected,
          this.yearSelected
        )
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    }
  }

  public monthChange(month: number) {
    this._chartService
      .getMonthlyChartDataByExerciseName(
        this.selectedExercise,
        month,
        this.yearSelected
      )
      .pipe(take(1))
      .subscribe((data: any) => {
        this.progressPercentage = Math.round(data.progressPercentage);
        if (data.data) this.chartInit(data as any);
        else this.chartOptions = {};
      });
  }

  public yearChange(year: number) {
    this.yearSelected = year;

    if (this.timeframeSelected == 'Monthly') {
      this._chartService
        .getMonthlyChartDataByExerciseName(
          this.selectedExercise,
          this.monthSelected,
          year
        )
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    } else {
      this._chartService
        .getYearlyChartDataByExerciseName(
          this.selectedExercise,
          this.yearSelected
        )
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    }
  }

  public onTimeframeChange(value: any) {
    this.timeframeSelected = value;

    if (this.timeframeSelected == 'Yearly') {
      this._chartService
        .getYearlyChartDataByExerciseName(
          this.selectedExercise,
          this.yearSelected
        )
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    } else {
      this._chartService
        .getMonthlyChartDataByExerciseName(
          this.selectedExercise,
          this.monthSelected,
          this.yearSelected
        )
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    }
  }

  private chartInit(data: any) {
    this.chartOptions = {
      series: [
        {
          name: this.selectedExercise,
          data: data.data,
        },
      ],
      chart: {
        animations: {
          speed: 400,
          animateGradually: {
            enabled: false,
          },
        },
        fontFamily: 'inherit',
        foreColor: 'inherit',
        width: '100%',
        height: '300px',
        type: 'line',
        toolbar: {
          show: false,
        },
        zoom: {
          enabled: false,
        },
      },
      colors: ['#818CF8', '#5CFF5C'],
      dataLabels: {
        enabled: false,
      },
      fill: {
        colors: ['#312E81'],
      },
      stroke: {
        curve: 'straight',
      },
      markers: {
        size: 5,
      },
      xaxis: {
        type: 'category',
        title: {
          text: this.timeframeSelected == 'Yearly' ? 'Months' : 'Days of month',
        },
        min: 1,
        max: this.timeframeSelected == 'Yearly' ? 12 : 30,
        tickAmount: this.timeframeSelected == 'Yearly' ? 11 : 29,
      },
      yaxis: {
        title: {
          text: 'Weight (kg)',
        },
        min: data.yMin,
        max: data.yMax,
        tickAmount: data.tickAmount,
      },
      legend: {
        position: 'top',
        horizontalAlign: 'right',
        floating: true,
        offsetY: -25,
        offsetX: -5,
      },
    };
  }
}
