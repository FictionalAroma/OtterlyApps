import { Component } from '@angular/core';
import { BingoCardDTO } from 'api/otterlyapi';
import { BingoCardService } from 'src/services/bingo-card.service';
import { BingoCardDisplayComponent } from '../bingo-card-display/bingo-card-display.component';

@Component({
  selector: 'app-bingo-card-list-view',
  templateUrl: './bingo-card-list-view.component.html',
  styleUrls: ['./bingo-card-list-view.component.scss']
})
export class BingoCardListViewComponent {

  constructor(private bingoService : BingoCardService){}

  public userCards : BingoCardDTO[] | undefined;

  ngOnInit()
  {
    this.bingoService.getCards().subscribe((cards : BingoCardDTO[]) => this.userCards = Array.from(cards))
  }
}
