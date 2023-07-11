import { TestBed } from '@angular/core/testing';

import { BingoGameService } from './bingo-game.service';

describe('BingoGameService', () => {
  let service: BingoGameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BingoGameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
