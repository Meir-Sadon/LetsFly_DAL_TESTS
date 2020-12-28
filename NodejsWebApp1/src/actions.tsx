import { AppEvents } from "./events"
import { UserTypes } from "./userTypes";

export interface IAction {
    type: AppEvents;
    payload: any;
}

export const SetUserType = (payload: string) => {
    return {
        type: AppEvents.SET_USER_TYPE,
        payload
    };
};

export const SetRegIdentity = (payload: UserTypes) => {
    return {
        type: AppEvents.SET_REGISTER_IDENTITY,
        payload
    };
};