import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { environment } from 'src/environments/environment';
import { OtterlyAppsUserDTO } from 'api/otterlyapi';
export interface UserAuthState {
  isAuthenticated: boolean;
  claims: string[] | undefined;
}

@Injectable()
export class LoginManagerService {
  constructor(private http: HttpClient) {}

  private user = new BehaviorSubject<UserAuthState>({
    isAuthenticated: false,
    claims: [],
  });

  private userProfile = new BehaviorSubject<OtterlyAppsUserDTO>({
    emailAddress: "",
    profileImagePath: "",
    twitchID: "",
    userID: "",
    userName: ""
  });


  userCache = this.user.asObservable();
  userProfileCache = this.userProfile.asObservable();

  getUser() {
    const myObserver = {
      next: (data: UserAuthState) => this.user.next(data),
      error: (err: Error) =>
        this.user.next({ isAuthenticated: false, claims: [] }),
    };
    this.http
      .get<UserAuthState>(`${environment.apiUrl}bff/auth/GetUserSignedIn`)
      .subscribe(myObserver);
  }

  login() {
    this.http.get(`${environment.apiUrl}bff/auth/login`).subscribe();
  }
  logout() {
    this.http.get(`${environment.apiUrl}bff/auth/login`).subscribe();
  }

  getUserProfile()
  {
    const myObserver = {
      next: (data: OtterlyAppsUserDTO) => this.userProfile.next(data),
      error: (err: Error) =>
        this.userProfile.next({
          emailAddress: "",
          profileImagePath: "",
          twitchID: "",
          userID: "",
          userName: ""
        }),
    };

    this.http.get<OtterlyAppsUserDTO>(`${environment.apiUrl}bff/user/profile`).subscribe(myObserver);
  }
}
