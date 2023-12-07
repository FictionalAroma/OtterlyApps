import './App.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import BingoConnector from './api/bingoconnector';
import TwitchConnector from "./api/twitchconnector";
import { useState, useEffect } from 'react';
import BingoApp from './components/bingoApp';
import config from './config.json'
import { Container } from 'react-bootstrap';

const apiConnector = new BingoConnector(config.API_BASE_URL);
const twitchConnector = new TwitchConnector();

function App() {


  const [extensionLoaded, setLoaded] = useState(false);
  const [bingoUser, setVerified] = useState();
  const [twitchUser, setUser] = useState();

  useEffect(() => {

    const setLoadingComplete = () => {
        setLoaded((l) => (l = bingoUser != null && twitchUser != null));
    }

    const onTokenValidated = (tokenUser) => {
        setVerified(tokenUser);
        setLoadingComplete();
    };
    const onTwitchUserGet = (twtichUser) =>{
        setUser(twtichUser);
        setLoadingComplete();
    }

    if(!config.LOCAL_TEST)
    {
        window.Twitch.ext.onAuthorized((auth)=> 
        {
            apiConnector.validateToken(auth.token, onTokenValidated);
            twitchConnector.setupUser(auth, onTwitchUserGet);
        }
    )}
    else
    {
      apiConnector.validateToken(config.LOCAL_JWT, onTokenValidated)
    };
  
    return () => {
      
    }
  }, [])

 
 
  if(extensionLoaded)
  {

    return(<Container fluid className="twitch-extension-container"><BingoApp api={apiConnector} twitchUser = {twitchUser}></BingoApp></Container>)
  }
  return <div className='loading'><h1>Loading....</h1></div>


}



export default App;
