import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoCardDisplayComponent } from './bingo-card-display.component';

describe('BingoCardDisplayComponent', () => {
  let component: BingoCardDisplayComponent;
  let fixture: ComponentFixture<BingoCardDisplayComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoCardDisplayComponent]
    });
    fixture = TestBed.createComponent(BingoCardDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
