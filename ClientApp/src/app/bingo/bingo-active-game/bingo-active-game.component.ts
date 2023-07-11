import { BaseResponse } from './../../../../api/otterlyapi.d';
import { Component } from '@angular/core';
import { BingoSessionDTO, BingoCardDTO } from 'api/otterlyapi';
import { BingoCardService } from 'src/services/bingo-card.service';

@Component({
  selector: 'app-bingo-active-game',
  templateUrl: './bingo-active-game.component.html',
  styleUrls: ['./bingo-active-game.component.scss']
})
export class BingoActiveGameComponent {
  public gameSession: BingoSessionDTO | undefined
  public userCards : BingoCardDTO[] | undefined;
  public selectedCardID : number = 0
  constructor(private bingoService : BingoCardService){

  }

  ngOnInit()
  {
    this.bingoService.getCurrentSessionObservable().subscribe((session : BingoSessionDTO) => this.gameSession = session);
    this.bingoService.getCardsObservable().subscribe((cards : Array<BingoCardDTO>) => this.userCards = Array.from(cards))

  }

  createSession(cardId: number)
  {
    this.bingoService.createSessionObservable(cardId).subscribe((session : BingoSessionDTO) => this.gameSession = session)
  }
endSession(sessionID : string)
{
    this.bingoService.endSessionObservable(sessionID).subscribe((resp:BaseResponse) => this.gameSession = resp.success ? undefined : this.gameSession);
    this.gameSession = undefined;
}
}
