'use strict';
var path = require('path');
var express = require('express');

var app = express();

var staticPath = path.join(__dirname, '/');
app.use(express.static(staticPath));

// Allows you to set port in the project properties.
app.set('port', process.env.PORT || 3000);

var server = app.listen(app.get('port'), function () {
    console.log('listening');
});



//var React = require('react');
//
//class MainNavbar extends React.Component {
//    public render() {
//        return (
//        <nav className="navbar navbar_expand_lg navbar_custom navbar_inverse">
//            <a className="navbar_brand" href="#/deals">LET'S FLY</a>
//            <button className="navbar_toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="true" aria-label="Toggle navigation">
//                <span className="navbar_toggler_icon"></span>
//            </button>
//
//            <div className="collapse navbar_collapse ml_auto" id="navbarSupportedContent">
//                <ul className="navbar_nav mr_auto">
//                    <li className="nav_item dropdown">
//                        <a className="nav_link dropdown_toggle dropdown_item" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
//                            Sign-Up
//                        </a>
//                        <div className="dropdown_menu" aria-labelledby="navbarDropdown">
//                            <a className="dropdown_item" ng-href="#/customer-register">As Customer</a><br />
//                            <a className="dropdown_item" ng-href="#/company-register">As Company</a><br />
//                        </div>
//                    </li>
//                    <li className="nav_item dropdown">
//                        <a className="nav_link dropdown_toggle" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
//                            Sign-In
//                        </a>
//                        <div className="dropdown_menu" aria-labelledby="navbarDropdown">
//                            <a className="dropdown_item" ng-href="#/customer-login">As Customer</a><br />
//                            <a className="dropdown_item" ng-href="#/company-login">As Company</a><br />
//                            <a className="dropdown_item" ng-href="#/administrator-login">As Administrator</a><br />
//                        </div>
//                    </li>
//                    <a className="nav_link dropdown_toggle dropdown_item" onClick="editSearchState(0)" href="page/searchflight">Search Flights</a>
//                </ul>
//            </div>
//        </nav>
//        );
//    }
//}
//
//export default MainNavbar