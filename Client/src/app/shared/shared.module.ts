import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { SideNavComponent } from './navigations/side-nav/side-nav.component';

@NgModule({
  declarations: [
    SideNavComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule
  ],
  exports: [LayoutComponent, SideNavComponent]
})
export class SharedModule { }
