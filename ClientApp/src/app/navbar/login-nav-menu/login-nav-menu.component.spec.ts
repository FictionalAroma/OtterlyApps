import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginNavMenuComponent } from './login-nav-menu.component';

describe('LoginNavMenuComponent', () => {
  let component: LoginNavMenuComponent;
  let fixture: ComponentFixture<LoginNavMenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoginNavMenuComponent]
    });
    fixture = TestBed.createComponent(LoginNavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
