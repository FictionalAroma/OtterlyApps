import './App.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import BingoConnector from './api/bingoconnector';
import { useState, useEffect } from 'react';
import BingoApp from './components/bingoApp';
import config from './config.json'
import { Container } from 'react-bootstrap';

const apiConnector = new BingoConnector(config.API_BASE_URL)

function App() {


  const [extensionLoaded, setLoaded] = useState(false);

  useEffect(() => {

    const onTokenValidated = (user) => {
        setLoaded(l => l = true);
    }

    if(!config.LOCAL_TEST)
    {
      window.Twitch.ext.onAuthorized((auth)=> apiConnector.validateToken(auth.token, onTokenValidated));
    }
    else
    {
      apiConnector.validateToken(config.LOCAL_JWT, onTokenValidated)
    };
  
    return () => {
      
    }
  }, [])

 
 
  if(extensionLoaded)
  {

    return(<Container fluid className="twitch-extension-container"><BingoApp api={apiConnector}></BingoApp></Container>)
  }
  return <h1>Loading....</h1>


}



export default App;
