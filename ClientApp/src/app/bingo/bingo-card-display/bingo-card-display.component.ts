import { BingoCardDTO } from 'api/otterlyapi';
import { BingoCardService } from './../../../services/bingo-card.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-bingo-card-display',
  templateUrl: './bingo-card-display.component.html',
  styleUrls: ['./bingo-card-display.component.scss']
})
export class BingoCardDisplayComponent {
  constructor(private bingoService : BingoCardService){}

  public userCards : BingoCardDTO[] | undefined;

  ngOnInit()
  {
    this.bingoService.getCards().subscribe((cards : BingoCardDTO[]) => this.userCards = {...cards})
  }
}
