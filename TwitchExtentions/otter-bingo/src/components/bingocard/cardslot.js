import {Card, Button}  from 'react-bootstrap';
import './cardgrid.scss'

export default function CardSlot({passedCell, onSelectCallback})
{
    const updateSelected=()=>
    {
        passedCell.selected = !passedCell.selected;
        onSelectCallback(passedCell);
    }

    return (
        <Card className={`bingo-slot-parent ${passedCell.selected ? "selected" : ""}`}>
        <Card.Body className={`bingo-slot-content`}>
        <Card.Title>{passedCell.displayText}</Card.Title>
        <Button variant="primary" className="centre" onClick={()=>updateSelected()}>{`${passedCell.selected ?  `Unselect` : `Select`}`}</Button>
        </Card.Body>
        </Card>

    )
}