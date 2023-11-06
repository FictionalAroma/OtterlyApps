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
        <Button className={`bingo-slot-parent ${passedCell.selected ? "selected" : ""}`} onClick={()=>updateSelected()}>
        {passedCell.displayText}
        </Button>


    )
}