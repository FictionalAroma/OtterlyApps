import { Component, Input, Output, EventEmitter } from '@angular/core';
import { BingoCardDTOImp } from 'api/bingoApiImp';
import { BingoCardDTO } from 'api/otterlyapi';


@Component({
  selector: 'app-bingo-card-list-view',
  templateUrl: './bingo-card-list-view.component.html',
  styleUrls: ['./bingo-card-list-view.component.scss']
})
export class BingoCardListViewComponent {

  @Input()
  userCards : BingoCardDTOImp[] | undefined;

  @Output() newCardEvent = new EventEmitter<BingoCardDTOImp>();
  @Output() deleteCardEvent = new EventEmitter<BingoCardDTOImp>();
  @Output() saveCardEvent = new EventEmitter<BingoCardDTOImp>();

  ngOnInit()
  {
  }

  saveCardDetails(updatedCard: BingoCardDTOImp) {
    this.saveCardEvent.emit(updatedCard);
  }
  addNewCard() {
    let newCard :BingoCardDTOImp = new BingoCardDTOImp();
    this.newCardEvent.emit(newCard);
  }

  deleteCard(updatedCard: BingoCardDTOImp)
  {
    this.deleteCardEvent.emit(updatedCard)
  }



}
