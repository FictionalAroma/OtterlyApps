import { Card, Button } from 'react-bootstrap';


export default function SessionItem({sessionItem, onCheckedCallback}) {
    
    const updateSelected=()=>
    {
        sessionItem.verified = !sessionItem.verified
        onCheckedCallback(sessionItem);
    }

    return (
        <Card>
        <Card.Body className={`${sessionItem.verified ? "selected" : ""}`}>
        <Card.Title>{sessionItem.displayText}</Card.Title>
        <Button variant="primary" onClick={()=>updateSelected()}>{`${sessionItem.verified ? "Verify" : "UnVerify"}`}</Button>
        </Card.Body>
        </Card>

    )

}
