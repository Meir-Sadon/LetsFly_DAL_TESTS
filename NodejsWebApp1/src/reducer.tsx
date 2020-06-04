import { AppEvents } from "./events";
import { IAction } from "./actions";

const initState = {
    name: "",
    age: 123
};

export interface IState {
    name: string;
    age: number;
}

export const reducer = (state: IState = initState, action: IAction) => {
    switch (action.type) {
        case AppEvents.SET_NAME:
            return { ...state, name: action.payload };
        case AppEvents.SET_AGE:
            return { ...state, age: action.payload };

        default:
            return state;
    }
};