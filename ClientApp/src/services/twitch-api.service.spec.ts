/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TwitchApiService } from './twitch-api.service';

describe('Service: TwitchApi', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TwitchApiService]
    });
  });

  it('should ...', inject([TwitchApiService], (service: TwitchApiService) => {
    expect(service).toBeTruthy();
  }));
});
