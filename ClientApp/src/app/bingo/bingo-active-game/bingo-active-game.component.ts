import { BingoCardService } from 'src/services/bingo-card.service';
import { Component, Input } from '@angular/core';
import { BingoSessionDTO, BingoCardDTO, BaseResponse, BingoSessionMetaDTO } from 'api/otterlyapi';
import { BingoGameService } from 'src/services/bingo-game.service';
import {FormControl, Validators, FormGroup} from '@angular/forms';
import { BingoCardDTOImp, BingoSessionMetaDTOImp } from 'api/bingoApiImp';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-bingo-active-game',
  templateUrl: './bingo-active-game.component.html',
  styleUrls: ['./bingo-active-game.component.scss']
})
export class BingoActiveGameComponent {
  public gameSession: BingoSessionDTO | undefined
  public sessionMeta: BingoSessionMetaDTOImp | undefined

  @Input()
  public userCards : BingoCardDTOImp[] | undefined;
  public selectedCardID : number = 0;

  public createGameGroup = new FormGroup({
    cardSelect: new FormControl(undefined, [Validators.required])
  });
  constructor(public bingoGame : BingoGameService, private cardService : BingoCardService, private dateFormatter : DatePipe){  }

  ngOnInit()
  {
    this.bingoGame.getCurrentSessionObservable().subscribe((session : BingoSessionDTO) => this.updateSessionData(session))

  }

  updateSessionData(session : BingoSessionDTO)
  {
    this.gameSession = session;
    if(this.gameSession != null)
    {
      this.bingoGame.sessionMetaObservable(session.sessionID).subscribe((meta : BingoSessionMetaDTO) => this.sessionMeta = new BingoSessionMetaDTOImp(meta));
    }

  }

  getSessionStartDateString() : string | null
  {
    if(this.sessionMeta == null)
    {
      return "";
    }
    return this.dateFormatter.transform(this.sessionMeta.startDate, "dd-MM-yyyy hh:mm");
  }

  getSessionRuntime() :string | null
  {
    if(this.sessionMeta == null)
    {
      return "";
    }
    var elapsed = Date.now() - this.sessionMeta.startDate.valueOf();
    return elapsed.toLocaleString();

  }

  createSession(cardId: number)
  {
    this.bingoGame.createSessionObservable(cardId).subscribe((session : BingoSessionDTO) => this.gameSession = {...session})
  }
endSession(sessionID : string)
{
    this.bingoGame.endSessionObservable(sessionID).subscribe((resp:BaseResponse) => this.gameSession = resp.success ? undefined : this.gameSession);
    this.gameSession = undefined;
}
}
