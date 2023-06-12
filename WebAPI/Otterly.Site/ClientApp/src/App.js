import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import { useAuth } from './context/AuthContext';

let App = () => {

  const { isAuthenticated, login, logout } = useAuth();
    return (
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}

          <Route path='/login' element={() => {login(); return null} } />
          
          
          {//<Route path='/logout' element={() =><div>{ logout; return null() }</div>}></Route>
          }
          
        </Routes>
      </Layout>
    );
}

export default App;


/*
const App = () => {

  const { isAuthenticated, login, logout } = useAuth();

  return (
    <Layout>
        <Routes>
          {AppRoutes.map((route,  index) => {
            const { element, authenticate, ...rest } = route;
            return <Route key={index} {...rest} element={() => !authenticate || isAuthenticated ? element : () => {login(); return null}} />;
          })}
          <Route path='/login' element={() => { login(); return null }} />
          <Route path='/logout' element={() => { logout(); return null }}></Route>
        </Routes>
      </Layout>
    );
  }


  export default App;
*/