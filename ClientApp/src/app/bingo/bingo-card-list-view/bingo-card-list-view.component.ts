import { Component } from '@angular/core';
import { BingoCardDTO } from 'api/otterlyapi';
import { BingoCardService } from 'src/services/bingo-card.service';


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
    this.bingoService.getCardsObservable().subscribe((cards : Array<BingoCardDTO>) => this.userCards = Array.from(cards))
  }

  saveCardDetails(updatedCard: BingoCardDTO) {
    this.bingoService.updateCard(updatedCard);
  }

}
