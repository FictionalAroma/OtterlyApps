import { BingoCardService } from 'src/services/bingo-card.service';
import { Component, Input } from '@angular/core';
import { BingoSessionDTO, BingoCardDTO, BaseResponse, BingoSessionMetaDTO } from 'api/otterlyapi';
import { BingoGameService } from 'src/services/bingo-game.service';


@Component({
  selector: 'app-bingo-active-game',
  templateUrl: './bingo-active-game.component.html',
  styleUrls: ['./bingo-active-game.component.scss']
})
export class BingoActiveGameComponent {
  public gameSession: BingoSessionDTO | undefined
  public sessionMeta: BingoSessionMetaDTO | undefined

  @Input()
  public userCards : BingoCardDTO[] | undefined;
  public selectedCardID : number = 0;
  constructor(public bingoGame : BingoGameService, private cardService : BingoCardService){
  }

  ngOnInit()
  {
    this.bingoGame.getCurrentSessionObservable().subscribe((session : BingoSessionDTO) => this.updateSessionData(session))

  }

  updateSessionData(session : BingoSessionDTO)
  {
    this.gameSession = session;
    if(this.gameSession != null)
    {
      this.bingoGame.sessionMetaObservable(session.sessionID).subscribe((meta : BingoSessionMetaDTO) => this.sessionMeta = meta);
    }

  }

  createSession(cardId: number)
  {
    this.bingoGame.createSessionObservable(cardId).subscribe((session : BingoSessionDTO) => this.gameSession = session)
  }
endSession(sessionID : string)
{
    this.bingoGame.endSessionObservable(sessionID).subscribe((resp:BaseResponse) => this.gameSession = resp.success ? undefined : this.gameSession);
    this.gameSession = undefined;
}
}
