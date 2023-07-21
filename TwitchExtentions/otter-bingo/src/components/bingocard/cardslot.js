import {Card, Button}  from 'react-bootstrap';
import './cardgrid.scss'
import { useState } from 'react';

export default function CardSlot({passedCell, onSelectCallback})
{
    const updateSelected=()=>
    {
        setCellValues({...cell, selected: !cell.selected})
        onSelectCallback(cell);
    }

    const [cell, setCellValues] = useState(passedCell);
    return (
        <Card className={`bingo-slot-parent ${cell.selected ? "selected" : ""}`}>
        <Card.Body className={`bingo-slot-content`}>
        <Card.Title>{cell.displayText}</Card.Title>
        <Button variant="primary" className="centre" onClick={()=>updateSelected()}>{`${cell.selected ?  `Unselect` : `Select`}`}</Button>
        </Card.Body>
        </Card>

    )
}