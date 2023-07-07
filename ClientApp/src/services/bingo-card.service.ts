import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse, BingoCardDTO, BingoSessionDTO } from 'api/otterlyapi';
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

  getCurrentSessionObservable()
  {
    return this.http.get<BingoSessionDTO>("bff/bingo/GetCurrentGame")
  }


  createSessionObservable(cardID: number) {
    return this.http.post<BingoSessionDTO>("bff/bingo/createSession", cardID)
  }

  endSessionObservable(sessionID: string) {
    return this.http.get<BaseResponse>("bff/bingo/endSession?sessionID=" + sessionID)
  }



}
