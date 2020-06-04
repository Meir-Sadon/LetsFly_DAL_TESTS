import { reducer as appReducer } from "./reducer";
import { combineReducers, createStore } from "redux";

const reducers = combineReducers({
    app: appReducer
});

export type IAppState = ReturnType<typeof reducers>;

export const store = createStore(reducers)