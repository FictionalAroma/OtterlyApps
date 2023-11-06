import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginManagerService } from 'src/services/login-manager.service';
import { OtterlyAppsUserDTO } from 'api/otterlyapi';
import { Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-login-nav-menu',
  templateUrl: './login-nav-menu.component.html',
  styleUrls: ['./login-nav-menu.component.scss']
})
export class LoginNavMenuComponent {
  public loggedIn :boolean;
  private loginService : LoginManagerService;
  public profile: OtterlyAppsUserDTO | undefined;
  constructor(@Inject(DOCUMENT) private document: Document, loginService : LoginManagerService)
  {
    this.loggedIn = false;
    this.loginService = loginService;
  }

  ngOnInit() {
    this.loginService.userCache.subscribe(u => {
      this.loggedIn = u.isAuthenticated;
      if(u.isAuthenticated) {
        this.loginService.getUserProfile();
    }});
    this.loginService.userProfileCache.subscribe(up => this.profile = up)
    this.loginService.getUser();
  }

  public getLoginUrl() :string{
    return `${environment.apiUrl}bff/auth/login`;
  }

  public getLogoutUrl() :string{
    return `${environment.apiUrl}bff/auth/logout`;
  }

  public login()
  {
    this.document.location.href=this.getLoginUrl();
  }

  public logout()
  {
    this.document.location.href=this.getLogoutUrl();
  }


}
