import logo from './logo.svg';
import './App.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import BingoConnector from './api/bingoconnector';
import { useState } from 'react';
import BingoApp from './components/bingoApp';
import CardGrid from './components/bingocard/cardgrid';

const apiConnector = new BingoConnector("https://localhost:7178/api")

function App() {

  const [extensionLoaded, setLoaded] = useState(false);

  if(!extensionLoaded)
  {
    window.Twitch.ext.onAuthorized((auth)=> apiConnector.validateToken(auth.token, 
      (user) => {
        if(!extensionLoaded){
          setLoaded(true);
        }
    }));
  }
 
  if(extensionLoaded)
  {
    return <BingoApp api={apiConnector}></BingoApp>;
  }
  return <h1>Loading....</h1>
}

export default App;
