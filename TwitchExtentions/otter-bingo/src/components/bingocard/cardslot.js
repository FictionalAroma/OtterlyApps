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
        <Card>
        <Card.Body className={`${cell.selected ? "selected" : ""}`}>
        <Card.Title>{cell.displayText}</Card.Title>
        <Button variant="primary" onClick={()=>updateSelected()}>Tick Item</Button>
        </Card.Body>
        </Card>

    )
}