import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router, RouterEvent } from '@angular/router';
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
  ApexLegend
} from "ng-apexcharts";
import { TrainingLog } from '../../models/TrainingLog';

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
  value: number,
  date: Date
}

@Component({
  selector: 'app-training-chart',
  templateUrl: './training-chart.component.html',
  styleUrls: ['./training-chart.component.scss']
})
export class TrainingChartComponent implements OnInit {

  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions!: Partial<ChartOptions | any>;

  @Input() chartData!: TrainingLog[];

  constructor() {}

  ngOnInit() {
    this.chartInit();
  }

  private chartInit() {
    const benchData = this.chartData.filter(log => log.exercises.length !== 0)
    .filter(log => log.exercises[0].name == 'Bench')
    .map(log => {
      return { y: log.exercises[0].weight, x: new Date(log.date).getDate() }
    });

    const cgbpData = this.chartData.filter(log => log.exercises.length !== 0)
    .filter(log => log.exercises[0].name == 'CGBP')
    .map(log => {
      return { y: log.exercises[0].weight, x: new Date(log.date).getDate() }
    }); 
  
    const benchTimes = this.chartData.map(log => new Date(log.date).getDate());

    this.chartOptions = {
      series: [
        {
          name: "Bench",
          data: benchData
        },
        {
          name: "CGBP",
          data: cgbpData
        }
      ],
      chart: {
        animations: {
          speed           : 400,
          animateGradually: {
              enabled: false
          }
        },
        fontFamily: 'inherit',
        foreColor : 'inherit',
        width     : '100%',
        height    : '300px',
        type      : 'line',
        toolbar   : {
            show: false
        },
        zoom      : {
            enabled: false
        }
      },
      colors    : ['#818CF8', '#5CFF5C'],
            dataLabels: {
                enabled: false
            },
            fill      : {
                colors: ['#312E81']
            },
      stroke: {
        curve: "straight"
      },
      title: {
        text: "Training Analytics",
        align: "left"
      },
      markers: {
        size: 5
      },
      xaxis: {
        type: 'category',
        title: {
          text: "Timeline"
        },
        min: 1,
        max: 26,
        tickAmount: 25
      },
      yaxis: {
        title: {
          text: "Weight"
        },
        min: 40,
        max: 62.5,
        tickAmount: 9
      },
      legend: {
        position: "top",
        horizontalAlign: "right",
        floating: true,
        offsetY: -25,
        offsetX: -5
      }
    };
  }
}
