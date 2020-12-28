"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.App = void 0;
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const MainNavbar_1 = require("./MainNavbar");
const LoginForm_1 = require("./LoginForm");
const bgStyles = {
    height: '800px',
    backgroundImage: "url('./../images/loginBackground.jpg')",
    padding: '10px',
    backgroundSize: 'cover'
};
exports.App = (props) => (React.createElement("div", { style: bgStyles, className: "stretchy" },
    React.createElement("button", { className: "btn btn-primary" }, "Click Me"),
    React.createElement(react_router_dom_1.BrowserRouter, null,
        React.createElement(react_router_dom_1.Route, { render: () => React.createElement(MainNavbar_1.default, { userType: props.userType }) }),
        React.createElement(react_router_dom_1.Switch, null,
            React.createElement(react_router_dom_1.Route, { path: ["/login-c"], render: () => React.createElement(LoginForm_1.LoginForm, null) })))));
//# sourceMappingURL=App.js.map