import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwitchigotchiPageComponent } from './twitchigotchi-page.component';

describe('TwitchigotchiPageComponent', () => {
  let component: TwitchigotchiPageComponent;
  let fixture: ComponentFixture<TwitchigotchiPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TwitchigotchiPageComponent]
    });
    fixture = TestBed.createComponent(TwitchigotchiPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
