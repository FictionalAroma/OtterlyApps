import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse, BingoCardDTO, BingoSessionDTO } from 'api/otterlyapi';
import { HttpHeaders } from '@angular/common/http';
import { BingoCardDTOImp } from 'api/bingoApiImp';
import { environment } from 'src/environments/environment';
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
    return this.http.get<BingoCardDTO[]>(`${environment.apiUrl}bff/bingo/getcards`)
  }

  updateCard(cardUpdate : BingoCardDTO)
  {
    this.http.post(`${environment.apiUrl}bff/bingo/UpdateCard`, JSON.stringify(cardUpdate), httpOptions).subscribe();
  }

  addCard(newCard: BingoCardDTOImp) {
    return this.http.put<BingoCardDTOImp>(`${environment.apiUrl}bff/bingo/AddCard`,JSON.stringify(newCard), httpOptions )
  }
  delete(newCard: BingoCardDTO) {

    let deleteOptions = {...httpOptions};
    deleteOptions.body = JSON.stringify(newCard);
    return this.http.delete<boolean>(`${environment.apiUrl}bff/bingo/DeleteCard`,deleteOptions)
  }




}
