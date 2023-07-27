import { BingoCardDTO, BingoSlotDTO } from 'api/otterlyapi';
import { Component, Input } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';
import { BingoCardDTOImp } from 'api/bingoApiImp';

@Component({
  selector: 'app-bingo-card-display',
  templateUrl: './bingo-card-display.component.html',
  styleUrls: ['./bingo-card-display.component.scss', '../bingo-display.scss']
})
export class BingoCardDisplayComponent {
  @Input() card : BingoCardDTOImp = new BingoCardDTOImp();
  @Output() cardSaveEvent = new EventEmitter<BingoCardDTOImp>;
  @Output() cardDeleteEvent = new EventEmitter<BingoCardDTOImp>;
  private cachedCard : BingoCardDTO = {
    cardID: 0,
    cardName: '',
    titleText: '',
    cardSize: 0,
    freeSpace: false,
    slots: []
  }

  ngOnInit()
  {
    this.card.slots = this.card.slots.sort((a, b) => a.slotIndex - b.slotIndex);
  }


  public isEditing : boolean = false;

  public startEdit()
  {
    this.isEditing = true;
    this.cachedCard = JSON.parse(JSON.stringify(this.card));
  }

  public discardChanges()
  {
    this.isEditing = false;
    this.card = new BingoCardDTOImp(JSON.parse(JSON.stringify(this.cachedCard)));
  }

  public saveChanges()
  {
    this.isEditing = false;
    this.cardSaveEvent.emit(this.card)
  }

  public deleteCard()
  {
    this.cardDeleteEvent.emit(this.card)
  }

  public updateFreeSpace(value : boolean)
  {
    this.card.freeSpace = value;
  }

  public addSlot()
  {

    var newIndex = 1;
    if(this.card.slots.length > 0)
    {
      var foundIndex = this.card.slots.at(this.card.slots.length -1)?.slotIndex
      if(foundIndex !== undefined)
      {
        newIndex = foundIndex + 1;
      }
    }

    var slot : BingoSlotDTO = {
      slotIndex: newIndex,
      cardID: this.card.cardID == undefined ? 0 : this.card.cardID,
      displayText: "",
      deleted: false,
    }
    this.card.slots.push({...slot})
  }
}
