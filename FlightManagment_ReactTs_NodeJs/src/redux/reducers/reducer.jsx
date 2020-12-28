import { AppEvents } from "../events/events";
import { IAction } from "../actions/actions";
import { UserTypes } from '../../enums/userTypes';

export interface IState {
    userType: string;
    identity: UserTypes;
}

const initState: IState = {
    userType: UserTypes.TYPE_4,
    identity: UserTypes.TYPE_4,
};


export const reducer = (state: IState = initState, action: IAction) => {
    switch (action.type) {
        case AppEvents.SET_USER_TYPE:
            return { ...state, userType: action.payload };
        case AppEvents.SET_REGISTER_IDENTITY:
            return { ...state, identity: action.payload };
        default:
            return state;
    }
};