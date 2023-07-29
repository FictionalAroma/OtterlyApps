import { Component, Input } from '@angular/core';
import { BingoSlotDTO } from 'api/otterlyapi';
import { Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-bingo-card-slot',
  templateUrl: './bingo-card-slot.component.html',
  styleUrls: ['./bingo-card-slot.component.scss', '../bingo-display.scss']
})
export class BingoCardSlotComponent {
  @Input() slot : BingoSlotDTO | undefined

  @Input() isEditing : boolean = false;

  @Input() index : number = 0;

  @Output() slotDeleteEvent = new EventEmitter<BingoSlotDTO>();
}
