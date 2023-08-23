
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse, BingoSessionDTO, BingoSessionMetaDTO } from 'api/otterlyapi';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BingoGameService {

  constructor(private http : HttpClient) {

   }

   getCurrentSessionObservable()
   {
     return this.http.get<BingoSessionDTO>(`${environment.apiUrl}bff/bingo/GetCurrentGame`)
   }


   createSessionObservable(cardID: number) {
     return this.http.post<BingoSessionDTO>(`${environment.apiUrl}bff/bingo/createSession`, cardID)
   }

   endSessionObservable(sessionID: string) {
     return this.http.get<BaseResponse>(`${environment.apiUrl}bff/bingo/endSession?sessionID=${sessionID}`)
   }

   VerifySlot(sessionID: string, itemIndex: number, state: boolean) {
    let reqest = {sessionID, itemIndex, state}
      return this.http.post(`${environment.apiUrl}bff/bingo/verifyItem`, reqest).subscribe();

   }

   sessionMetaObservable(sessionID:string)
   {
    return this.http.get<BingoSessionMetaDTO>(`${environment.apiUrl}bff/bingo/GetSessionMeta?sessionID=${sessionID}`)
  }


}
