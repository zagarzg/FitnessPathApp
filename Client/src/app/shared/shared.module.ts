import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialModule } from './material.module';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    MaterialModule
  ],
  exports: [
    CommonModule,
    FontAwesomeModule,
    MaterialModule
  ]
})
export class SharedModule { }
