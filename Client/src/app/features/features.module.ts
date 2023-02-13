import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { TrainingLogPageComponent } from './training-log/pages/training-log-page/training-log-page.component';
import { RecentLogsListComponent } from './training-log/components/recent-logs-list/recent-logs-list.component';
import { TrainingLogService } from './training-log/services/training-log.service';
import { ExerciseService } from './training-log/services/exercise.service';
import { ExerciseFormComponent } from './training-log/components/exercise-form/exercise-form.component';
import { TrainingChartComponent } from './training-log/components/training-chart/training-chart.component';
import { WeightLogPageComponent } from './weight-log/pages/weight-log-page/weight-log-page.component';
import { WeightLogListComponent } from './weight-log/components/weight-log-list/weight-log-list.component';

@NgModule({
  declarations: [
    TrainingLogPageComponent,
    RecentLogsListComponent,
    ExerciseFormComponent,
    TrainingChartComponent,
    WeightLogPageComponent,
    WeightLogListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    TrainingLogPageComponent
  ],
  providers: [
    TrainingLogService,
    ExerciseService
  ]
})
export class FeaturesModule { }
