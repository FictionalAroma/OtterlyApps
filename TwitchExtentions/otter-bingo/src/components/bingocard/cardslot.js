import {Card, Button}  from 'react-bootstrap';
import './cardgrid.scss'
import { useState } from 'react';

export default function CardSlot(props)
{


    const [cell, setCellValues] = useState(props.cell);
    return (
        <Card>
        <Card.Body className={`${cell.Selected ? "selected" : ""}`}>
        <Card.Title>{cell.DisplayText}</Card.Title>
        <Button variant="primary" onClick={()=>setCellValues({...cell, Selected: !cell.Selected})}>Tick Item</Button>
        </Card.Body>
        </Card>

    )
}