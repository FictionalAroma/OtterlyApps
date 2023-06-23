import { Component, Input } from '@angular/core';
import { BingoSlotDTO } from 'api/otterlyapi';

@Component({
  selector: 'app-bingo-card-slot',
  templateUrl: './bingo-card-slot.component.html',
  styleUrls: ['./bingo-card-slot.component.scss']
})
export class BingoCardSlotComponent {
  @Input() slot : BingoSlotDTO = {
    slotIndex: 0,
    cardID: 0,
    displayText: ''
  }

  @Input() isEditing : boolean = false;
}
