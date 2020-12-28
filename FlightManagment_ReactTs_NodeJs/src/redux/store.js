import { reducer as appReducer } from "./reducers/reducer";
import { combineReducers, createStore } from 'react-redux';

const reducers = combineReducers({
    app: appReducer
});

export type IAppState = ReturnType<typeof reducers>;

export const store = createStore(reducers)