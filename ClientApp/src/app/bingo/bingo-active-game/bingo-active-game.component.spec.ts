import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoActiveGameComponent } from './bingo-active-game.component';

describe('BingoActiveGameComponent', () => {
  let component: BingoActiveGameComponent;
  let fixture: ComponentFixture<BingoActiveGameComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoActiveGameComponent]
    });
    fixture = TestBed.createComponent(BingoActiveGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
