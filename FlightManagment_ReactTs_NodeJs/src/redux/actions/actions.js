"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SetRegIdentity = exports.SetUserType = void 0;
const events_1 = require("./../events/events");
exports.SetUserType = (payload) => {
    return {
        type: events_1.AppEvents.SET_USER_TYPE,
        payload
    };
};
exports.SetRegIdentity = (payload) => {
    return {
        type: events_1.AppEvents.SET_REGISTER_IDENTITY,
        payload
    };
};
//# sourceMappingURL=actions.js.map