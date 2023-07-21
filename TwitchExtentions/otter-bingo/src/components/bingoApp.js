import { Tabs, Tab } from "react-bootstrap"
import PlayerTab from "./playerTab"
import { useState } from "react"
import SessionControl from "./streameradmin/sessionControl";


export default function BingoApp({api})
{
    const [loaded, setLoaded] = useState(false);
    const [loading, setLoading] = useState(false);
    const [ticket, setTicket] = useState();
    const [session, setSession] = useState();

    if(!loading)
    {
        setLoading(true);
        api.getSessionAndTicket((data)=>{
            if(!loaded)
            {
                setLoaded(true)
            }
                setTicket(data.playerTicket);
                setSession(data.session);
            })
    }

    if(!loaded)
    {
        return <h1>Loading....</h1>
    }

    const playerTab = <PlayerTab api={api} ticket={ticket} session={session} ticketCreateCallback={setTicket}></PlayerTab>;
    const user = api.user();
    if(user.userRole === 2 || user.userRole === 3)
    {
        return (    
            <Tabs
            defaultActiveKey="card"
            id="justify-tab-example"
            className="mb-3"
            justify>
                <Tab eventKey="card" title="Card">    
                    {playerTab}
                </Tab>
                <Tab eventKey="streamer" title="Game Admin">
                    <SessionControl api={api} session={session}></SessionControl>
                </Tab>
            </Tabs>
        )
    }
    return playerTab;   
}