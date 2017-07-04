import React from 'react';
import ReactDOM from 'react-dom';
//import { browserHistory, Router } from 'react-router';
import { App } from './routes.jsx';


ReactDOM.render(
    <App />,
    document.getElementById('app')
);

if (module.hot){
    module.hot.accept();
}
