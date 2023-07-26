import { Component, Input, Output, EventEmitter } from '@angular/core';
import { BingoCardDTO } from 'api/otterlyapi';
import { BingoCardService } from 'src/services/bingo-card.service';


@Component({
  selector: 'app-bingo-card-list-view',
  templateUrl: './bingo-card-list-view.component.html',
  styleUrls: ['./bingo-card-list-view.component.scss']
})
export class BingoCardListViewComponent {

  @Input()
  userCards : BingoCardDTO[] | undefined;

  @Output() newCardEvent = new EventEmitter<BingoCardDTO>();
  @Output() deleteCardEvent = new EventEmitter<BingoCardDTO>();
  @Output() saveCardEvent = new EventEmitter<BingoCardDTO>();

  ngOnInit()
  {
  }

  saveCardDetails(updatedCard: BingoCardDTO) {
    this.saveCardEvent.emit(updatedCard);
  }
  addNewCard() {
    let newCard :BingoCardDTO = {
      cardID: undefined,
      cardName: "Card for Streaming",
      cardSize: 5,
      freeSpace: true,
      titleText: "My Awesome Card!",
      slots:[],
    }
    this.newCardEvent.emit(newCard);
  }

  deleteCard(updatedCard: BingoCardDTO)
  {
    this.deleteCardEvent.emit(updatedCard)
  }



}
