import { Component, Input, OnChanges, ViewChild } from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';

export type ChartOptions = {
  series: number[];
  labels: string[];
  chart: {
    type: string;
  };
  plotOptions: {
    pie: {
      donut: {
        labels: {
          show: boolean;
          value: Function;
        };
        total: {
          show: boolean;
          label: string;
          formatter: Function;
        };
      };
    };
  };
};

@Component({
  selector: 'app-food-chart',
  templateUrl: './food-chart.component.html',
  styleUrls: ['./food-chart.component.scss'],
})
export class FoodChartComponent implements OnChanges {
  @ViewChild('chart') chart!: ChartComponent;
  chartOptions: Partial<ChartOptions | any> = {};

  @Input() chartData!: number[] | null;

  constructor() {}

  ngOnChanges() {
    if (this.chartData) {
      this.chartInit(this.chartData);
    } else {
      this.chartOptions = {};
    }
  }

  private chartInit(data: any) {
    let calories = data.pop();
    this.chartOptions = {
      series: data,
      labels: ['Carbs', 'Protein', 'Fat'],
      chart: {
        type: 'donut',
      },
      plotOptions: {
        pie: {
          donut: {
            labels: {
              show: true,
              value: {
                formatter: function (val: number) {
                  return val + ' g';
                },
              },
              total: {
                show: true,
                label: 'Total Calories',
                formatter: function () {
                  return calories + ' kcal';
                },
              },
            },
          },
        },
      },
    };
  }
}
