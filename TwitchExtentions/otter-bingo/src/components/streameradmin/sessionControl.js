import { Container } from 'react-bootstrap'
import SessionItem from './sessionItem'
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function SessionControl({api, session}) {

    const onItemChanged = (sessionItem) => {
        api.verifyItem(sessionItem.itemIndex, session.sessionID, sessionItem.verified)
    }
    const size = 4;
    const rows = new Array((session.sessionItems.length/4) +1);
    for(let i = 0; i < session.sessionItems.length; i+=size)
    {
      rows.push(session.sessionItems.slice(i, i+size))
    }

    const gridContents = rows.map((row, index)=> 
    {
      const rowContents = row.map((cell)=>
        <Col key={cell.itemIndex}>
            <SessionItem sessionItem={cell} onCheckedCallback={onItemChanged}></SessionItem>
        </Col>
          );
        return <Row key={index}>{rowContents}</Row>
      }
    )
    

    return (      <Container fluid>
        {gridContents}
      </Container>

        
    )

}
