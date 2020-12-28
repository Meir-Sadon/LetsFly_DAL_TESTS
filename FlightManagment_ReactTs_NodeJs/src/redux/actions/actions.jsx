import { AppEvents } from "./../events/events"
import { UserTypes } from "../../enums/userTypes";

export const SetUserType = (payload) => {
    return {
        type: AppEvents.SET_USER_TYPE,
        payload
    };
};

export const SetRegIdentity = (payload) => {
    return {
        type: AppEvents.SET_REGISTER_IDENTITY,
        payload
    };
};