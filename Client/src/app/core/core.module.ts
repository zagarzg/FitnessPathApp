import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LayoutComponent } from './layout/layout.component';
import { SideNavComponent } from './navigations/side-nav/side-nav.component';
import { SharedModule } from '../shared/shared.module';
import { CurvedOutsideBarComponent } from './navigations/curved-outside-bar/curved-outside-bar.component';

@NgModule({
  declarations: [LayoutComponent, SideNavComponent, CurvedOutsideBarComponent],
  imports: [CommonModule, RouterModule, SharedModule, HttpClientModule],
  exports: [LayoutComponent, SideNavComponent, CurvedOutsideBarComponent],
})
export class CoreModule {}
