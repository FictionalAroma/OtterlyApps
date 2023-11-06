export interface BaseRequest {
  userID: string;
}
export interface BaseResponse {
  success: boolean;
  error: string;
}
export interface GetCardDetailsResponse extends BaseResponse {
  card: BingoCardDTO;
  cardFields: BingoSlotDTO[];
}
export interface UpdateCardDetailsRequest extends BaseRequest {
  cardDetails: BingoCardDTO;
}



export interface BingoCardDTO {
  cardID: number | undefined;
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
  deleted: boolean;
}

export interface OtterlyAppsUserDTO {
  userID: string;
  twitchID: string;
  userName: string;
  profileImagePath: string;
  emailAddress: string;
}

export interface BingoSessionDTO {
  size: number;
  freeSpace: boolean;
  sessionItems: BingoSessionItemDTO[];
  active: boolean;
  cardTitle: string;
  sessionID: string;
}

export interface BingoSessionMetaDTO {
  numberTickets: number;
  numberWinners: number;
  startDate: Date;
}

export interface BingoSessionItemDTO {
  itemIndex: number;
  sessionID: string;
  displayText: string;
  verified: boolean;
}
