import { AppEvents } from "./events"

export interface IAction {
    type: AppEvents;
    payload: any;
}

export const SetName = (payload: string) => {
    return {
        type: AppEvents.SET_NAME,
        payload
    };
};

export const SetAge = (payload: number) => {
    return {
        type: AppEvents.SET_AGE,
        payload
    };
};