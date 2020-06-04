import React from 'react';
import RenderDOM from 'react-dom';
import { App } from './src/container'
import { Provider } from 'react-redux';
import { store } from './src/store';

RenderDOM.render(<Provider store={store}><App /></Provider>, document.getElementById("root"));