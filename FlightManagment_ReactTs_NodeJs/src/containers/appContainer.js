"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.App = void 0;
const App_1 = require("./../components/App");
const react_redux_1 = require("react-redux");
const actions_1 = require("./../redux/actions/actions");
const store_1 = require("./../redux/store");
const userTypes_1 = require("./../enums/userTypes");
const axios_1 = require("axios");
const sweetalert2_1 = require("sweetalert2");
function trySubmit(userName, password) {
    axios_1.default.post("https://localhost:951/api/Auth", {
        User_Name: userName,
        Password: password
    })
        .then((res) => {
        console.log(res);
        switch (JSON.parse(res.data).type) {
            case userTypes_1.UserTypes.TYPE_1:
                actions_1.SetUserType(userTypes_1.UserTypes.TYPE_1);
                break;
            case userTypes_1.UserTypes.TYPE_2:
                actions_1.SetUserType(userTypes_1.UserTypes.TYPE_2);
                break;
            case userTypes_1.UserTypes.TYPE_3:
                actions_1.SetUserType(userTypes_1.UserTypes.TYPE_3);
                break;
            default:
                actions_1.SetUserType(userTypes_1.UserTypes.TYPE_4);
        }
        sweetalert2_1.default.fire("Successfully Connected", `You Will Immadiately Be Taken To The Requested Page.`, 'success');
    }, (err) => {
        console.log(err.response);
        sweetalert2_1.default.fire(`Login Failed`, `${err.response.data}`, "error");
    });
}
exports.default = trySubmit;
const askWhichUserToRegister = () => {
    sweetalert2_1.default.fire({
        title: 'Choose Identity',
        icon: 'question',
        html: getHtmlForChooseIdentityMessage(),
        showCancelButton: false,
        showConfirmButton: false,
        onClose: onIdentitySwalClosed
    });
    function onIdentitySwalClosed() {
        console.log(store_1.store.getState().app.identity);
        switch (store_1.store.getState().app.identity) {
            case userTypes_1.UserTypes.TYPE_2:
                axios_1.default.post("https://localhost:951/homepage/cusreg");
                break;
            case userTypes_1.UserTypes.TYPE_3:
                axios_1.default.post("https://localhost:951/homepage/cmpreg");
                break;
            default:
        }
    }
    $("#btnRegCst").click(function () {
        actions_1.SetRegIdentity(userTypes_1.UserTypes.TYPE_3);
    });
    $("#btnRegCmp").click(function () {
        actions_1.SetRegIdentity(userTypes_1.UserTypes.TYPE_2);
    });
};
const handleRestorePassword = () => { console.log("Im From Handle Restore Password"); };
function getHtmlForChooseIdentityMessage() {
    return 'Are You Want To Register As Customer Or Company?' + "<br>" + "<br>" +
        `<a href="https://localhost:951/#/customerregister"><button type="button" id="btnRegCst" role="button" tabindex="0" class="btn-primary m-5" style="height:30px" >` + `As Customer` + `</button> </a>` +
        `<a href="https://localhost:951/#/companyregister"><button type="button" id="btnRegCmp" role="button" tabindex="0" class="btn-primary m-5" style="height:30px" >` + 'As Company' + '</button> </a>';
}
exports.App = react_redux_1.connect((state) => ({
    userType: state.app.userType,
    identity: state.app.identity
}), {
    SetUserType: actions_1.SetUserType, SetRegIdentity: actions_1.SetRegIdentity
})(App_1.App);
//# sourceMappingURL=appContainer.js.map