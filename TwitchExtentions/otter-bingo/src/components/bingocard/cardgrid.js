import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import CardSlot from './cardslot';
import './cardgrid.scss'



export default function CardGrid() {
    const exampleCard = {
        _id: {
          $oid: "64ad72e9767843f167bd42b4"
        },
        TwitchUserID: "123456",
        SessionID: {
          $oid: "64ad47765b79a6a83282fb6b"
        },
        Slots: [
          {
            ItemIndex: 7,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test5",
            Verified: true,
            Selected: false
          },
          {
            ItemIndex: 8,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "WHY!?",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 1,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test1",
            Verified: true,
            Selected: false
          },
          {
            ItemIndex: 10,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test8",
            Verified: false,
            Selected: true
          },
          {
            ItemIndex: 15,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "fghfdsjlghfskldgd",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 17,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "This is an Insert Test",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 9,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test7",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 16,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "123456",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 5,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "TestInsert",
            Verified: true,
            Selected: false
          },
          {
            ItemIndex: 6,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test4",
            Verified: true,
            Selected: false
          },
          {
            ItemIndex: 13,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test11",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 14,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test12",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 12,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test10",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 2,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test2",
            Verified: false,
            Selected: false
          },
          {
            ItemIndex: 11,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test9",
            Verified: true,
            Selected: false
          }
        ]
      }
    const exampleSession = {
        _id: {
          $oid: "64ad47765b79a6a83282fb6b"
        },
        UserID: {
          $binary: {
            base64: "+cbVJhn9qE6YrtJcJj+KRA==",
            subType: "03"
          }
        },
        TwitchUserID: "74527936",
        CardTitle: "Fictional Dev Stream Bingo",
        Size: 4,
        FreeSpace: true,
        SessionItems: [
          {
            ItemIndex: 1,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test1",
            Verified: true
          },
          {
            ItemIndex: 2,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test2",
            Verified: false
          },
          {
            ItemIndex: 5,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "TestInsert",
            Verified: true
          },
          {
            ItemIndex: 6,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test4",
            Verified: true
          },
          {
            ItemIndex: 7,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test5",
            Verified: true
          },
          {
            ItemIndex: 8,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "WHY!?",
            Verified: false
          },
          {
            ItemIndex: 9,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test7",
            Verified: false
          },
          {
            ItemIndex: 10,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test8",
            Verified: false
          },
          {
            ItemIndex: 11,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test9",
            Verified: true
          },
          {
            ItemIndex: 12,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test10",
            Verified: false
          },
          {
            ItemIndex: 13,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test11",
            Verified: false
          },
          {
            ItemIndex: 14,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "Test12",
            Verified: false
          },
          {
            ItemIndex: 15,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "fghfdsjlghfskldgd",
            Verified: false
          },
          {
            ItemIndex: 16,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "123456",
            Verified: false
          },
          {
            ItemIndex: 17,
            SessionID: {
              $oid: "64ad47765b79a6a83282fb6b"
            },
            DisplayText: "This is an Insert Test",
            Verified: false
          }
        ],
        Active: true,
        Meta: {
          NumberTickets: 1,
          NumberWinners: 0
        }
      }
      let size = exampleSession.Size
      let rows = new Array(size);
      for(let i = 0; i < exampleCard.Slots.length; i+=size)
      {
        rows.push(exampleCard.Slots.slice(i, i+size))
      }

      let gridContents = rows.map((row, index)=> 
      {
        let rowContents = row.map((cell)=>
        <Col key={cell.ItemIndex}>
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
  
