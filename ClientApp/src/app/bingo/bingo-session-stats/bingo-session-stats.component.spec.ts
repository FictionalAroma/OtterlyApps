import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoSessionStatsComponent } from './bingo-session-stats.component';

describe('BingoSessionStatsComponent', () => {
  let component: BingoSessionStatsComponent;
  let fixture: ComponentFixture<BingoSessionStatsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoSessionStatsComponent]
    });
    fixture = TestBed.createComponent(BingoSessionStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
