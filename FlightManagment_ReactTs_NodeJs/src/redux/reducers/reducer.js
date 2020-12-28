"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.reducer = void 0;
const events_1 = require("./../events/events");
const userTypes_1 = require("../../enums/userTypes");
const initState = {
    userType: userTypes_1.UserTypes.TYPE_4,
    identity: userTypes_1.UserTypes.TYPE_4,
};
exports.reducer = (state = initState, action) => {
    switch (action.type) {
        case events_1.AppEvents.SET_USER_TYPE:
            return Object.assign(Object.assign({}, state), { userType: action.payload });
        case events_1.AppEvents.SET_REGISTER_IDENTITY:
            return Object.assign(Object.assign({}, state), { identity: action.payload });
        default:
            return state;
    }
};
//# sourceMappingURL=reducer.js.map