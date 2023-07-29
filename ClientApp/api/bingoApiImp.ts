import { BingoCardDTO, BingoSlotDTO } from './otterlyapi';
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
