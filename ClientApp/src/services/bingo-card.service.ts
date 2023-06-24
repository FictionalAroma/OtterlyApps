import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BingoCardDTO } from 'api/otterlyapi';
import { HttpHeaders } from '@angular/common/http';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',})}

@Injectable({
  providedIn: 'root'
})
export class BingoCardService {
  constructor(private http: HttpClient) {
  }

  getCardsObservable()
  {
    return this.http.get<BingoCardDTO[]>("bff/bingo/getcards")
  }

  updateCard(cardUpdate : BingoCardDTO)
  {
    this.http.post("bff/bingo/UpdateCard", JSON.stringify(cardUpdate), httpOptions).subscribe();
  }
}
