import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BingoGamePageComponent } from './bingo-game-page.component';

describe('BingoGamePageComponent', () => {
  let component: BingoGamePageComponent;
  let fixture: ComponentFixture<BingoGamePageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BingoGamePageComponent]
    });
    fixture = TestBed.createComponent(BingoGamePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
