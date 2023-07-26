import { Component } from '@angular/core';
import { BingoCardDTO, BingoSessionDTO } from 'api/otterlyapi';
import { BingoCardService } from 'src/services/bingo-card.service';

@Component({
  selector: 'app-bingo-game-page',
  templateUrl: './bingo-game-page.component.html',
  styleUrls: ['./bingo-game-page.component.scss']
})
export class BingoGamePageComponent {
 public userCards: BingoCardDTO[] | undefined;
 constructor(private cardService : BingoCardService)
 {
}

 ngOnInit()
 {
  this.cardService.getCardsObservable().subscribe((cards : Array<BingoCardDTO>) => this.userCards = Array.from(cards))
 }

 saveCardDetails(updatedCard: BingoCardDTO) {
  this.cardService.updateCard(updatedCard);
}
addNewCard(newCard: BingoCardDTO) {
  this.cardService.addCard(newCard).subscribe((addedCard : BingoCardDTO) => this.userCards?.push(addedCard));
}

deleteCard(updatedCard: BingoCardDTO)
{
  this.cardService.delete(updatedCard).subscribe((success : boolean) => {
    if(success)
    {
      this.userCards?.splice(this.userCards.findIndex((v:BingoCardDTO)=> v.cardID == updatedCard.cardID),1);
    }
  })
}

}
