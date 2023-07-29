import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfilePageComponent } from './profile/profile-page/profile-page.component';
import { BingoGamePageComponent } from './bingo/bingo-game-page/bingo-game-page.component';
import { TwitchigotchiPageComponent } from './twitchigotchi/twitchigotchi-page/twitchigotchi-page.component';

const routes: Routes = [
  {path: 'bingo', component: BingoGamePageComponent},
  {path: 'profile', component: ProfilePageComponent},
  {path: 'twitchygotchi', component: TwitchigotchiPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
