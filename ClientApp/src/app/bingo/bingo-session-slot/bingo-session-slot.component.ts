import { BingoGameService } from './../../../services/bingo-game.service';
import { BingoSessionDTO, BingoSessionItemDTO, BingoSlotDTO } from 'api/otterlyapi';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-bingo-session-slot',
  templateUrl: './bingo-session-slot.component.html',
  styleUrls: ['./bingo-session-slot.component.scss']
})
export class BingoSessionSlotComponent {
  @Input() session : BingoSessionDTO = {
    size: 0,
    freeSpace: false,
    sessionItems: [],
    active: false,
    cardTitle: '',
    sessionID: ''
  }

  @Input()  gameService!: BingoGameService;
  constructor()
  {

  }

  updateVerified(slot : BingoSessionItemDTO) {
    this.gameService.VerifySlot(this.session.sessionID, slot.itemIndex, slot.verified);
  }

}
