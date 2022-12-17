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
  public chartOptions!: Partial<ChartOptions | any>;

  @Input() chartData!: TrainingLog[];

  exercises: string[] = ['Bench', 'CGBP'];
  selectedExercise: string = 'Bench';

  constructor(private _chartService: ChartService) {}

  ngOnInit() {
    this.exerciseChange('Bench');
  }

  public exerciseChange(exerciseName: string) {
    this._chartService
      .getChartDataByExerciseName(exerciseName, 10)
      .pipe(take(1))
      .subscribe((data) => {
        console.log(data);
        this.chartInit(data as any);
      });
  }

  private chartInit(data: any) {
    this.chartOptions = {
      series: [
        {
          name: 'Bench',
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
          text: 'Timeline',
        },
        min: 1,
        max: 26,
        tickAmount: 25,
      },
      yaxis: {
        title: {
          text: 'Weight',
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
