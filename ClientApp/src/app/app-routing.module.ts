import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BingoCardListViewComponent } from './bingo/bingo-card-list-view/bingo-card-list-view.component';
import { ProfilePageComponent } from './profile/profile-page/profile-page.component';
import { BingoGamePageComponent } from './bingo/bingo-game-page/bingo-game-page.component';

const routes: Routes = [
  {path: 'bingo', component: BingoGamePageComponent},
  {path: 'profile', component: ProfilePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
