import { Button } from "react-bootstrap";
import CardGrid from "./bingocard/cardgrid";

export default function PlayerTab({api, ticket, session})
{
    if(ticket === undefined)
    {
        return <Button onClick={()=>api.CreateTicket()}>Get Bingo Ticket</Button>
    }
    return <CardGrid ticket={ticket} size={session.size}></CardGrid>

}
