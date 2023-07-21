import { useState } from 'react';
import { Card, Button } from 'react-bootstrap';


export default function SessionItem({sessionItem, onCheckedCallback}) {
    
    const updateSelected=()=>
    {
        var updatedItem = item;
        updatedItem.verified = !updatedItem.verified
        setCellValues({...updatedItem})
        onCheckedCallback(updatedItem);
    }

    const [item, setCellValues] = useState(sessionItem);
    return (
        <Card>
        <Card.Body className={`${item.verified ? "selected" : ""}`}>
        <Card.Title>{item.displayText}</Card.Title>
        <Button variant="primary" onClick={()=>updateSelected()}>{`${item.verified ? "Verify" : "UnVerify"}`}</Button>
        </Card.Body>
        </Card>

    )

}
