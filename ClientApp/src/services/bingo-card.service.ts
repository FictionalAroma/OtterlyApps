import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BingoCardDTO } from 'api/otterlyapi';
import { HttpHeaders } from '@angular/common/http';
const httpOptions = {
  headers: new HttpHeaders({'Content-Type':'application/json',}),
  body: ""
  }

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

  addCard(newCard: BingoCardDTO) {
    return this.http.put<BingoCardDTO>("bff/bingo/AddCard",JSON.stringify(newCard), httpOptions )
  }
  delete(newCard: BingoCardDTO) {

    let deleteOptions = {...httpOptions};
    deleteOptions.body = JSON.stringify(newCard);
    return this.http.delete<boolean>("bff/bingo/DeleteCard",deleteOptions)


  }

}
