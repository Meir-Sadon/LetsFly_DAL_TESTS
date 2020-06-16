import { AppEvents } from "./events"

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