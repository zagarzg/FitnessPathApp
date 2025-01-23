import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
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
import { Observable } from 'rxjs';
import { WeightChartFilters } from 'src/app/features/training-log/services/chart.service';

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

  @Input() chartData!: Observable<any>;
  @Output() filtersChangeEvent = new EventEmitter<WeightChartFilters>();

  months: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
  years: number[] = [2024, 2025];
  progressPercentage: number = 0;

  weightChartFilters: WeightChartFilters = {
    isMonthly: true,
    yearSelected: new Date().getFullYear(),
    monthSelected: new Date().getMonth() + 1,
  };

  constructor() {}

  ngOnInit() {
    this.chartData.subscribe((chartData) => {
      this.chartInit(chartData, 'Monthly');
      this.progressPercentage = Math.round(chartData.progressPercentage);
    });
  }

  onFiltersChange(weightChartFilters: WeightChartFilters) {
    this.filtersChangeEvent.emit(weightChartFilters);
  }

  private chartInit(data: any, timeframeSelected: string) {
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
