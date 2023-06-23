import { BingoCardDTO } from 'api/otterlyapi';
import { BingoCardService } from './../../../services/bingo-card.service';
import { Component, Input } from '@angular/core';

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
  }

  private cachedCard : BingoCardDTO = {
    cardID: 0,
    userID: '',
    cardName: '',
    titleText: '',
    cardSize: 0,
    freeSpace: false,
    slots: []
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
}
