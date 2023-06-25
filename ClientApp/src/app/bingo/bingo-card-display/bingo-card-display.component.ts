import { BingoCardDTO, BingoSlotDTO } from 'api/otterlyapi';
import { Component, Input } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-bingo-card-display',
  templateUrl: './bingo-card-display.component.html',
  styleUrls: ['./bingo-card-display.component.scss']
})
export class BingoCardDisplayComponent {
  @Input() card : BingoCardDTO = {
    cardID: 0,
    userID: '',
    cardName: '',
    titleText: '',
    cardSize: 0,
    freeSpace: false,
    slots: []
  };
  @Output() cardSaveEvent = new EventEmitter<BingoCardDTO>;

  private cachedCard : BingoCardDTO = {
    cardID: 0,
    userID: '',
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
    this.cachedCard = {...this.card};
  }

  public discardChanges()
  {
    this.isEditing = false;
    this.card = {...this.cachedCard};
  }

  public saveChanges()
  {
    this.isEditing = false;
    this.cardSaveEvent.emit(this.card)
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
      cardID: this.card.cardID,
      displayText: "",
    }
    this.card.slots.push({...slot})
  }
}
