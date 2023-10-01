import { BingoCardDTO, BingoSessionMetaDTO, BingoSlotDTO } from './otterlyapi';
export class BingoCardDTOImp implements BingoCardDTO
{
  cardID: number | undefined;
  cardName: string = "Card for Streaming";
  titleText: string = "My Awesome Card!";
  cardSize: number = 3;
  freeSpace: boolean = false;
  slots: BingoSlotDTO[] = [];

  constructor(private seedData: BingoCardDTO | undefined = undefined)
  {
    if(seedData != null)
    {
      this.cardID = seedData.cardID;
      this.cardName = seedData.cardName;
      this.titleText = seedData.titleText;
      this.cardSize = seedData.cardSize;
      this.freeSpace = seedData.freeSpace;
      this.slots = Array.from(seedData.slots);
    }
  }

  isCardValid() : boolean
  {

    return this.slots.length >= this.getMinSlotCount();
  }


  public getMinSlotCount()
  {
    let minCount = Math.pow(this.cardSize,2);
    if(this.freeSpace)
    {
      minCount -= 1;
    }

    return minCount;
  }

}

export class BingoSessionMetaDTOImp implements BingoSessionMetaDTO
{
  numberTickets: number = 0;
  numberWinners: number = 0;
  startDate: Date = new Date();

  public constructor(private seedData:BingoSessionMetaDTO )
  {
    this.numberTickets = seedData.numberTickets;
    this.numberWinners = seedData.numberWinners;
    this.startDate = seedData.startDate;
  }
  public getRuntime() : Date
  {
    return new Date(Date.now() - this.startDate.valueOf())
  }
}

