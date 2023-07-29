import { Tabs, Tab } from "react-bootstrap"
import PlayerTab from "./playerTab"
import { useEffect, useRef, useState } from "react"
import SessionControl from "./streameradmin/sessionControl";
import "../App.scss"


export default function BingoApp({api})
{
    const [initialLoad, setLoaded] = useState(false)
    const [ticket, setTicket] = useState();
    const [session, setSession] = useState();
    const ticketRef = useRef(null);
    const sessionRef = useRef(null);

    const updateStateFromRef =() =>
    {
        setSession(s => s = sessionRef.current);
        setTicket(t => t = ticketRef.current);
    }

    const updateSessionData = (target, contentType, message)=>
    {
        if(ticketRef.current != null && sessionRef.current != null)
        {
            const payload = JSON.parse(message)
            const updatedTicket = {...ticketRef.current};
            for (let index = 0; index < updatedTicket.slots.length; index++) {
                const s = updatedTicket.slots[index];
                if(s.itemIndex === payload.ItemIndex)
                {
                    s.verified = payload.State;
                    break;
                }
            };
            
            ticketRef.current = updatedTicket;
            const updatedSession = {...session}
            for (let index = 0; index < updatedSession.sessionItems.length; index++) {
                const s = updatedSession.sessionItems[index];
                if(s.itemIndex === payload.ItemIndex)
                {
                    s.verified = payload.State;
                    break;
                }
            };
            sessionRef.current = updatedSession;
            
            updateStateFromRef();
        }
    };

    const cellSelected=(cell)=>
    {
        api.markCellSelected(cell.itemIndex, session.sessionID)
        const updatedTicket = {...ticketRef.current};
        for (let index = 0; index < updatedTicket.slots.length; index++) {
            const s = updatedTicket.slots[index];
            if(s.itemIndex === cell.itemIndex)
            {
                updatedTicket.slots[index] = {...cell};
                break;
            }
        };
        
        ticketRef.current = updatedTicket;
        updateStateFromRef();

    } 

    useEffect(() => {
          
        api.getSessionAndTicket((data)=>{

            sessionRef.current = data.session;
            ticketRef.current = data.playerTicket;

            setLoaded(b => b = true);
            updateStateFromRef();

        });

      return () => {
    }
    }, [api])

    useEffect(() => {

        if(initialLoad)
        {
            window.Twitch.ext.listen("broadcast", (target, contentType, message) => updateSessionData(target, contentType, message) );
        }
    
      return () => {
        }
    }, [initialLoad] )
  
     

    if(ticket == null && session == null)
    {
        return <h1>Loading....</h1>
    }


    
    const playerTab = 
        <PlayerTab 
            className="tab-content h-100 d-inline-block row" 
            api={api} 
            ticket={ticket} 
            session={session} 
            ticketCreateCallback={(ticket) => {
                                                ticketRef.current = ticket;
                                                updateStateFromRef()
                                            } }
            cellSelectedCallback={cellSelected}>
        </PlayerTab>;
    const user = api.user();
    if(session != null && (user.userRole === 2 || user.userRole === 3))
    {
        return (    
            <Tabs
            defaultActiveKey="card"
            id="justify-tab-example"
            className="mb-3"
            justify
            >
                <Tab className="twitch-extension-tab" eventKey="card" title="Card">   
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