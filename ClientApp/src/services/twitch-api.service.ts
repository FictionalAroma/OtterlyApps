import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
const httpOptions = {
  headers: new HttpHeaders({'Content-Type':'application/json',}),
  body: ""
  }

@Injectable({
  providedIn: 'root'
})


export class TwitchApiService {

  constructor(private http:HttpClient) { }

  //getTwitchLoginLink
}
