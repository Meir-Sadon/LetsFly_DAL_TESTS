import { AppEvents } from "./events";
import { IAction } from "./actions";
import { UserTypes } from "./userTypes";

const initState: IState = {
    userType: UserTypes.TYPE_4,
};

export interface IState {
    userType: string;
}

export const reducer = (state: IState = initState, action: IAction) => {
    switch (action.type) {
        case AppEvents.SET_USER_TYPE:
            return { ...state, userType: action.payload };
        default:
            return state;
    }
};