import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/layout/layout.component';
import { TrainingLogPageComponent } from './features/training-log/pages/training-log-page/training-log-page.component';

const routes: Routes = [
  { 
    path: '',
    component: LayoutComponent,
    children: [
      { 
        path: 'training-log', component: TrainingLogPageComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
