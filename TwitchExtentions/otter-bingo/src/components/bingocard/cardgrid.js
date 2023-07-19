import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import CardSlot from './cardslot';
import './cardgrid.scss'



export default function CardGrid({ticket, size}) {

      let rows = new Array(size);
      for(let i = 0; i < ticket.slots.length; i+=size)
      {
        rows.push(ticket.slots.slice(i, i+size))
      }

      let gridContents = rows.map((row, index)=> 
      {
        let rowContents = row.map((cell)=>
        <Col key={cell.itemIndex}>
            <CardSlot cell={cell}></CardSlot>
        </Col>
            
            )
      return <Row key={index}>{rowContents}</Row>
        }
      )

    return (
        <Container fluid>
        {gridContents}
      </Container>
    );

  }
  
