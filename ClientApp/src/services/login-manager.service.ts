import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
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

  userCache = this.user.asObservable();


  getUser() {
    const myObserver = {
      next: (data: UserAuthState) => this.user.next(data),
      error: (err: Error) =>
        this.user.next({ isAuthenticated: false, claims: [] }),
    };
    this.http
      .get<UserAuthState>('bff/auth/GetUserSignedIn')
      .subscribe(myObserver);
  }

  login() {
    this.http.get('bff/auth/login').subscribe();
  }
  logout() {
    this.http.get('bff/auth/login').subscribe();
  }
}
