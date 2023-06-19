import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BingoCardDTO } from 'api/otterlyapi';

@Injectable({
  providedIn: 'root'
})
export class BingoCardService {

  constructor(private http: HttpClient) {
  }

  getCards()
  {
    return this.http.get<BingoCardDTO[]>("bff/bingo/getcards")
  }
}
