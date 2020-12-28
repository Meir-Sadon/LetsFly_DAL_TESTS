"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const userTypes_1 = require("../enums/userTypes");
const MainNav = require('react-bootstrap');
const MainNavbar = ({ userType }) => (React.createElement("div", { style: { padding: '2px' } },
    React.createElement(MainNav.Navbar, { collapseOnSelect: true, expand: "lg", className: "navbar_inverse" },
        React.createElement(MainNav.Navbar.Brand, null,
            React.createElement(react_router_dom_1.Link, { to: "/home" }),
            "LET'S FLY"),
        React.createElement(MainNav.Navbar.Toggle, { "aria-controls": "responsive-navbar-nav", style: { backgroundImage: 'url(./../images/hamburger_icon.jpg)', height: '30px', width: '30px', backgroundSize: '35px', backgroundPosition: 'center' } }),
        React.createElement(MainNav.Navbar.Collapse, { id: "responsive-navbar-nav" },
            React.createElement(MainNav.Nav, { className: "ml-auto" },
                React.createElement("div", { style: { display: userType == userTypes_1.UserTypes.TYPE_1 ? 'flex' : 'none' } },
                    React.createElement(MainNav.NavDropdown, { title: "Management", id: "collasible-nav-dropdown" },
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/edit-companies" }, "Edit Airlines"),
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/edit-customers" }, "Edit Customers")),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/admin-inbox" }, "Inbox"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/search-flights" }, "Search Flights"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/admin-profile" }, "Profile"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/home" }, "Log out")),
                React.createElement("div", { style: { display: userType == userTypes_1.UserTypes.TYPE_2 ? 'flex' : 'none' } },
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/own-flights" }, "Own Flights"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/company-inbox" }, "Inbox"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/search-flights" }, "Search Flights"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/company-profile" }, "Profile"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/home" }, "Log out")),
                React.createElement("div", { style: { display: userType == userTypes_1.UserTypes.TYPE_3 ? 'flex' : 'none' } },
                    React.createElement(MainNav.NavDropdown, { title: "Manage My Tickets", id: "collasible-nav-dropdown" },
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/buy-ticket" }, "Buy New Ticket"),
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/edit-tickets" }, "Show/Edit/Remove")),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/customer-inbox" }, "Inbox"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/search-flights" }, "Search Flights"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/customer-profile" }, "Profile"),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/home" }, "Log out")),
                React.createElement("div", { style: { display: userType == userTypes_1.UserTypes.TYPE_4 ? 'flex' : 'none' } },
                    React.createElement(MainNav.NavDropdown, { title: "Sign-Up", id: "collasible-nav-dropdown" },
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/page/customerregister" }, "As Customer"),
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/page/companyregister" }, "As Company")),
                    React.createElement(MainNav.NavDropdown, { title: "Sign-In", id: "collasible-nav-dropdown" },
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/logincustomer" }, "As Customer"),
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/logincompany" }, "As Company"),
                        React.createElement(MainNav.NavDropdown.Divider, null),
                        React.createElement(react_router_dom_1.Link, { className: "dropdown-item", to: "/adminlogin" }, "As Administrator")),
                    React.createElement(react_router_dom_1.Link, { className: "nav-link nav-link", to: "/page/searchflight" }, "Search Flights")))))));
exports.default = MainNavbar;
//# sourceMappingURL=MainNavbar.js.map