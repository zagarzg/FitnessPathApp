import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { TrainingLogPageComponent } from './training-log/pages/training-log-page/training-log-page.component';



@NgModule({
  declarations: [
    TrainingLogPageComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    TrainingLogPageComponent
  ]
})
export class FeaturesModule { }
