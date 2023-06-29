import { Component } from '@angular/core';
import { LoginManagerService } from 'src/services/login-manager.service';

@Component({
  selector: 'app-login-nav-menu',
  templateUrl: './login-nav-menu.component.html',
  styleUrls: ['./login-nav-menu.component.scss']
})
export class LoginNavMenuComponent {
  public loggedIn :boolean;
  private loginService : LoginManagerService;
  constructor(loginService : LoginManagerService)
  {
    this.loggedIn = false;
    this.loginService = loginService;
  }

  ngOnInit() {
    this.loginService.userCache.subscribe(u => this.loggedIn = u.isAuthenticated);
    this.loginService.getUser();
  }
}
