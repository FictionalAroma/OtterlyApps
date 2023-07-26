import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import CardSlot from './cardslot';
import './cardgrid.scss'



export default function CardGrid({ticket, size, cellSelectedCallback}) {

      const rows = new Array(size);
      for(let i = 0; i < ticket.slots.length; i+=size)
      {
        rows.push(ticket.slots.slice(i, i+size))
      }

      const gridContents = rows.map((row, index)=> 
      {
        const rowContents = row.map((cell)=>
          <Col key={cell.itemIndex}>
              <CardSlot passedCell={cell} onSelectCallback={cellSelectedCallback}></CardSlot>
          </Col>
            );
          return <Row key={index} className='bingo-row'>{rowContents}</Row>
        }
      )

  

    return (
      <>
        {gridContents}
      </>
    );

  }
  
