import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialModule } from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgApexchartsModule } from 'ng-apexcharts';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    NgApexchartsModule
  ],
  exports: [
    CommonModule,
    FontAwesomeModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    NgApexchartsModule
  ]
})
export class SharedModule { }
