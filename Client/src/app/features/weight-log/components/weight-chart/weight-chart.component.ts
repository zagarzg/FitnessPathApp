import { Component, Input, OnInit, ViewChild } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexGrid,
  ApexLegend,
  ApexMarkers,
  ApexStroke,
  ApexTitleSubtitle,
  ApexXAxis,
  ApexYAxis,
  ChartComponent,
} from 'ng-apexcharts';
import { ChartService } from 'src/app/features/training-log/services/chart.service';
import { WeightLog } from '../../models/WeightLog';
import { take } from 'rxjs/operators';

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
  selector: 'app-weight-chart',
  templateUrl: './weight-chart.component.html',
  styleUrls: ['./weight-chart.component.scss'],
})
export class WeightChartComponent implements OnInit {
  @ViewChild('chart') chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions | any> = {};

  @Input() chartData!: WeightLog[] | null;

  months: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
  years: number[] = [2021, 2022, 2023];
  monthSelected: number = new Date().getMonth() + 1;
  yearSelected: number = new Date().getFullYear();
  timeframeSelected: 'Monthly' | 'Yearly' = 'Monthly';
  progressPercentage: number = 0;

  constructor(private _chartService: ChartService) {}

  ngOnInit() {
    this.monthChange(this.monthSelected);
  }

  public monthChange(month: number) {
    this._chartService
      .getMonthlyWeightChangeData(month, this.yearSelected)
      .pipe(take(1))
      .subscribe((data: any) => {
        console.log(data);
        this.progressPercentage = Math.round(data.progressPercentage);
        if (data.data) this.chartInit(data as any);
        else this.chartOptions = {};
      });
  }

  public yearChange(year: number) {
    this.yearSelected = year;
    if (this.timeframeSelected == 'Monthly') {
      this._chartService
        .getMonthlyWeightChangeData(this.monthSelected, year)
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    } else {
      this._chartService
        .getYearlyWeightChangeData(this.yearSelected)
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
        .getYearlyWeightChangeData(this.yearSelected)
        .pipe(take(1))
        .subscribe((data: any) => {
          this.progressPercentage = Math.round(data.progressPercentage);
          if (data.data) this.chartInit(data as any);
          else this.chartOptions = {};
        });
    } else {
      this._chartService
        .getMonthlyWeightChangeData(this.monthSelected, this.yearSelected)
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
          name: 'Weight',
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
      colors: ['#111827', '#5CFF5C'],
      dataLabels: {
        enabled: false,
      },
      fill: {
        colors: ['#111827'],
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
        tickAmount: 2,
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
