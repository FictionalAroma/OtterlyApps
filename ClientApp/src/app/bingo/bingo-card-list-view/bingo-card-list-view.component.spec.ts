import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoCardListViewComponent } from './bingo-card-list-view.component';

describe('BingoCardListViewComponent', () => {
  let component: BingoCardListViewComponent;
  let fixture: ComponentFixture<BingoCardListViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoCardListViewComponent]
    });
    fixture = TestBed.createComponent(BingoCardListViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
