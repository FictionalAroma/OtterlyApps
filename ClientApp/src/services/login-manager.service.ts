import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
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
      .get<UserAuthState>(`${environment.apiUrl}bff/auth/GetUserSignedIn`)
      .subscribe(myObserver);
  }

  login() {
    this.http.get(`${environment.apiUrl}bff/auth/login`).subscribe();
  }
  logout() {
    this.http.get(`${environment.apiUrl}bff/auth/login`).subscribe();
  }
}
