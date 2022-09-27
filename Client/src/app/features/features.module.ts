import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { TrainingLogPageComponent } from './training-log/pages/training-log-page/training-log-page.component';
import { RecentLogsListComponent } from './training-log/components/lists/recent-logs-list/recent-logs-list.component';
import { TrainingLogService } from './training-log/services/training-log.service';

@NgModule({
  declarations: [
    TrainingLogPageComponent,
    RecentLogsListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    TrainingLogPageComponent
  ],
  providers: [TrainingLogService]
})
export class FeaturesModule { }
