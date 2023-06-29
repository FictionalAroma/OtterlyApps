import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BingoCardListViewComponent } from './bingo/bingo-card-list-view/bingo-card-list-view.component';

const routes: Routes = [
  {path: 'bingo', component: BingoCardListViewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
