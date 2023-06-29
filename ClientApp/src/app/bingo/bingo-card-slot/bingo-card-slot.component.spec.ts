import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoCardSlotComponent } from './bingo-card-slot.component';

describe('BingoCardSlotComponent', () => {
  let component: BingoCardSlotComponent;
  let fixture: ComponentFixture<BingoCardSlotComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoCardSlotComponent]
    });
    fixture = TestBed.createComponent(BingoCardSlotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
