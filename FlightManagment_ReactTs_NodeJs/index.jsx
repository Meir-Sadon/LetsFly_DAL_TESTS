import * as React from 'react';
const ReactDOM = require('react-dom');
import { App } from './src/containers/appContainer'
const Provider = require('react-redux');
import { store } from './src/redux/store';


ReactDOM.render(<Provider store={ store } > <App /></Provider >, $("#root"));
