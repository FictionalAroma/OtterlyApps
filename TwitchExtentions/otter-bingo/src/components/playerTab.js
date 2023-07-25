import { Button } from "react-bootstrap";
import CardGrid from "./bingocard/cardgrid";
import WinScreen from "./bingocard/winscreen";

export default function PlayerTab({api, ticket, session, ticketCreateCallback, cellSelectedCallback})
{

    if(ticket == null)
    {
        return <Button onClick={()=>api.createTicket(ticketCreateCallback)}>Get Bingo Ticket</Button>
    }

    const winner = ticket.slots.every(s => s.verified === true && s.selected === true);

    if(winner)
    {
        return <WinScreen></WinScreen>;
    }

    return <CardGrid ticket={ticket} size={session.size} cellSelectedCallback={cellSelectedCallback} ></CardGrid>

}
