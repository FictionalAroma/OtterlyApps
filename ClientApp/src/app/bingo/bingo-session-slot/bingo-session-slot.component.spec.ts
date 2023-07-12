import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoSessionSlotComponent } from './bingo-session-slot.component';

describe('BingoSessionSlotComponent', () => {
  let component: BingoSessionSlotComponent;
  let fixture: ComponentFixture<BingoSessionSlotComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoSessionSlotComponent]
    });
    fixture = TestBed.createComponent(BingoSessionSlotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
