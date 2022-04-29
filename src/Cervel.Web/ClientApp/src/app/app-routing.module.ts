import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TimeParserPageComponent } from './pages/time-parser-page/time-parser-page.component';

const routes: Routes = [
  {
    path: 'time-parser',
    children: [
      { path: '', component: TimeParserPageComponent, },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
