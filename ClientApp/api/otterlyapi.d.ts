export interface BaseRequest {
  userID: string;
}
export interface BaseResponse {
  success: boolean;
  error: string;
}


export interface BingoCardDTO {
  cardID: number;
  userID: string;
  cardName: string;
  titleText: string;
  cardSize: number;
  freeSpace: boolean;
  slots: BingoSlotDTO[];
}

export interface BingoSlotDTO {
  slotIndex: number;
  cardID: number;
  displayText: string;
}

export interface GetCardDetailsResponse extends BaseResponse {
  card: BingoCardDTO;
  cardFields: BingoSlotDTO[];
}
export interface UpdateCardDetailsRequest extends BaseRequest {
  cardDetails: BingoCardDTO;
}
export interface OtterlyAppsUserDTO {
  userID: string;
  test: number;
  deleted: boolean;
  twitchID: string;
}
