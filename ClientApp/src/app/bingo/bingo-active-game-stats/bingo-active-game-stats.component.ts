import { Component, Input } from '@angular/core';
import { BingoSessionDTO, BingoSessionMetaDTO } from 'api/otterlyapi';
import { BingoGameService } from 'src/services/bingo-game.service';

@Component({
  selector: 'app-bingo-active-game-stats',
  templateUrl: './bingo-active-game-stats.component.html',
  styleUrls: ['./bingo-active-game-stats.component.scss']
})
export class BingoActiveGameStatsComponent {
  @Input() meta : BingoSessionMetaDTO | undefined = {numberTickets: 0, numberWinners: 0}
  constructor()
  {

  }
}
