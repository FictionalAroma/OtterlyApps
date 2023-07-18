import logo from './logo.svg';
import './App.scss';
import CardGrid from './components/bingocard/cardgrid';
import 'bootstrap/dist/css/bootstrap.min.css';
import BingoConnector from './api/bingoconnector';

function App() {
  let apiConnector = new BingoConnector("https://localhost:7178/api")
  window.Twitch.ext.onAuthorized((auth)=> apiConnector.validateToken(auth.token))
  return (

    <CardGrid></CardGrid>
  );
}

export default App;
