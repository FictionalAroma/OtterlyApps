import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import {CLogin} from "./components/CLogin"
import {CLogout} from "./components/CLogout"


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    authenticate: true,
    element: <FetchData />
  },
  {
    path: '/login',
    element: <CLogin />
  },
  {
    path: '/logout',
    element: <CLogout />
  },

];

export default AppRoutes;
