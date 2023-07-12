import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoActiveGameStatsComponent } from './bingo-active-game-stats.component';

describe('BingoActiveGameStatsComponent', () => {
  let component: BingoActiveGameStatsComponent;
  let fixture: ComponentFixture<BingoActiveGameStatsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoActiveGameStatsComponent]
    });
    fixture = TestBed.createComponent(BingoActiveGameStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
