import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/layout/layout.component';
import { TrainingLogPageComponent } from './features/training-log/pages/training-log-page/training-log-page.component';
import { WeightLogPageComponent } from './features/weight-log/pages/weight-log-page/weight-log-page.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: 'training-log',
        component: TrainingLogPageComponent,
      },
      {
        path: 'weight-log',
        component: WeightLogPageComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
