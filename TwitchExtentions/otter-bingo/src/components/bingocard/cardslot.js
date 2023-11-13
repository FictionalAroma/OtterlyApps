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
        <td
            className={`word-break ${passedCell.selected ? "selected" : ""}`}
            onClick={() => updateSelected()}>
            <strong>{passedCell.displayText}</strong>
        </td>
    );
}