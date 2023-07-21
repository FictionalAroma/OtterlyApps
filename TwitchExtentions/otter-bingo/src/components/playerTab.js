import { Button } from "react-bootstrap";
import CardGrid from "./bingocard/cardgrid";

export default function PlayerTab({api, ticket, session, ticketCreateCallback})
{
    const cellSelected=(cell)=>
    {
        api.markCellSelected(cell.itemIndex, session.sessionID)
    } 

    if(ticket == null)
    {
        return <Button onClick={()=>api.createTicket(ticketCreateCallback)}>Get Bingo Ticket</Button>
    }
    return <CardGrid ticket={ticket} size={session.size} cellSelectedCallback={cellSelected} ></CardGrid>

}
