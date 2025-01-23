import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
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
import { TrainingChartFilters } from '../../services/chart.service';
import { Observable } from 'rxjs';

export interface ChartOptions {
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
}

@Component({
  selector: 'app-training-chart',
  templateUrl: './training-chart.component.html',
  styleUrls: ['./training-chart.component.scss'],
})
export class TrainingChartComponent implements OnInit {
  @ViewChild('chart') chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions | any> = {};

  @Input() chartData!: Observable<any>;
  @Input() exercises!: string[] | null;

  @Output() filtersChangeEvent = new EventEmitter<TrainingChartFilters>();

  months: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
  years: number[] = [2024, 2025];
  progressPercentage: number = 0;

  constructor() {}

  ngOnInit() {
    this.chartData.subscribe((chartData) => {
      this.chartInit(chartData, 'Bench Press', 'Monthly');
    });
  }

  onFiltersChange(trainingChartFilters: TrainingChartFilters) {
    this.filtersChangeEvent.emit(trainingChartFilters);
  }

  private chartInit(
    data: any,
    exerciseSelected: string,
    timeframeSelected: string
  ) {
    this.chartOptions = {
      series: [
        {
          name: exerciseSelected,
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
      colors: ['#2196f3', '#5CFF5C'],
      dataLabels: {
        enabled: false,
      },
      fill: {
        colors: ['#2196f3'],
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
          text: timeframeSelected == 'Yearly' ? 'Months' : 'Days of month',
        },
        min: 1,
        max: timeframeSelected == 'Yearly' ? 12 : 30,
        tickAmount: timeframeSelected == 'Yearly' ? 11 : 29,
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
