declare var require: any

import * as React from 'react';
const MainNav = require('react-bootstrap');
import 'bootstrap/dist/css/bootstrap.css';


class MainNavbar extends React.Component {
    render() {
        return (
            <div style={{ padding: '2px' }}>
            <MainNav.Navbar collapseOnSelect expand="lg" className="navbar_inverse">
                <MainNav.Navbar.Brand href="#home">LET'S FLY</MainNav.Navbar.Brand>
                    <MainNav.Navbar.Toggle aria-controls="responsive-navbar-nav" style={{backgroundImage: 'url(./../images/hamburger_icon.jpg)', height: '30px', width: '30px', backgroundSize: '35px', backgroundPosition: 'center'}}/>
                    <MainNav.Navbar.Collapse id="responsive-navbar-nav">
                    <MainNav.Nav className="ml-auto">
                        <MainNav.NavDropdown title="Sign-Up" id="collasible-nav-dropdown">
                            <MainNav.NavDropdown.Item href="#customer/register">As Customer</MainNav.NavDropdown.Item>
                            <MainNav.NavDropdown.Item href="#company/register">As Company</MainNav.NavDropdown.Item>
                        </MainNav.NavDropdown>
                        <MainNav.NavDropdown title="Sign-In" id="collasible-nav-dropdown">
                            <MainNav.NavDropdown.Item href="#customer/login">As Customer</MainNav.NavDropdown.Item>
                            <MainNav.NavDropdown.Item href="#company/login">As Company</MainNav.NavDropdown.Item>
                            <MainNav.NavDropdown.Divider />
                            <MainNav.NavDropdown.Item href="#administrator/login">As Administrator</MainNav.NavDropdown.Item>
                        </MainNav.NavDropdown>
                        <MainNav.Nav.Link href="#pricing">Search Flights</MainNav.Nav.Link>
                    </MainNav.Nav>
                </MainNav.Navbar.Collapse>
                </MainNav.Navbar>      
            </div>
            );
    }
}

export default MainNavbar











//var React = require('react');

//class MainNavbar extends React.Component {
//    public render() {
//        return (
//            <nav className="navbar navbar_expand_lg navbar_custom navbar_inverse">
//                <a className="navbar_brand" href="#/deals">LET'S FLY</a>
//                <button className="navbar_toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="true" aria-label="Toggle navigation">
//                    <span className="navbar_toggler_icon"></span>
//                </button>

//                <div className="collapse navbar_collapse ml_auto" id="navbarSupportedContent">
//                    <ul className="navbar_nav mr_auto">
//                        <li className="nav_item dropdown">
//                            <a className="nav_link dropdown_toggle dropdown_item" style={{ color: '#007bff' }} id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
//                                Sign-Up
//                        </a>
//                            <div className="dropdown_menu" aria-labelledby="navbarDropdown">
//                                <a className="dropdown_item" ng-href="#/customer-register">As Customer</a><br />
//                                <a className="dropdown_item" ng-href="#/company-register">As Company</a><br />
//                            </div>
//                        </li>
//                        <li className="nav_item dropdown">
//                            <a className="nav_link dropdown_toggle" style={{ color: '#007bff'}} id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
//                                Sign-In
//                        </a>
//                            <div className="dropdown_menu" aria-labelledby="navbarDropdown">
//                                <a className="dropdown_item" ng-href="#/customer-login">As Customer</a><br />
//                                <a className="dropdown_item" ng-href="#/company-login">As Company</a><br />
//                                <a className="dropdown_item" ng-href="#/administrator-login">As Administrator</a><br />
//                            </div>
//                        </li>
//                        <a className="nav_link dropdown_toggle dropdown_item" onClick="editSearchState(0)" href="page/searchflight">Search Flights</a>
//                    </ul>
//                </div>
//            </nav>
//        );
//    }
//}

//export default MainNavbar










//var React = require('react');
//var myNav = require('mdbreact');
//import { BrowserRouter as Router } from 'react-router-dom';



//class MainNavbar extends React.Component {
//state = {
//  collapseID: ""
//    };

//toggleCollapse = collapseID => () =>
//  this.setState(prevState => ({
//  collapseID: prevState.collapseID !== collapseID ? collapseID : ""
//}));

//render() {
//  return (
//    <Router>
//      <myNav.MDBContainer>
//        <myNav.MDBNavbar color="red" dark expand="md" style={{ marginTop: "20px" }}>
//          <myNav.MDBNavbarBrand>
//            <strong className="white-text">myNav.MDBNavbar</strong>
//          </myNav.MDBNavbarBrand>
//          <myNav.MDBNavbarToggler onClick={this.toggleCollapse("navbarCollapse3")} />
//          <myNav.MDBCollapse id="navbarCollapse3" isOpen={this.state.collapseID} navbar>
//            <myNav.MDBNavbarNav left>
//              <myNav.MDBNavItem active>
//                <myNav.MDBNavLink to="#!">Home</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBNavLink to="#!">Features</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBNavLink to="#!">Pricing</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBDropdown>
//                  <myNav.MDBDropdownToggle nav caret>
//                    <div className="d-none d-md-inline">myNav.MDBDropdown</div>
//                  </myNav.MDBDropdownToggle>
//                  <myNav.MDBDropdownMenu right>
//                    <myNav.MDBDropdownItem href="#!">Action</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Another Action</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Something else here</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Something else here</myNav.MDBDropdownItem>
//                  </myNav.MDBDropdownMenu>
//                </myNav.MDBDropdown>
//              </myNav.MDBNavItem>
//            </myNav.MDBNavbarNav>
//            <myNav.MDBNavbarNav right>
//              <myNav.MDBNavItem>
//                <myNav.MDBFormInline waves>
//                  <div className="md-form my-0">
//                    <input className="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search" />
//                  </div>
//                </myNav.MDBFormInline>
//              </myNav.MDBNavItem>
//            </myNav.MDBNavbarNav>
//          </myNav.MDBCollapse>
//        </myNav.MDBNavbar>
//        <myNav.MDBNavbar color="secondary-color" dark expand="md" style={{ marginTop: "20px" }}>
//          <myNav.MDBNavbarBrand>
//            <strong className="white-text">myNav.MDBNavbar</strong>
//          </myNav.MDBNavbarBrand>
//          <myNav.MDBNavbarToggler onClick={this.toggleCollapse("navbarCollapse3")} />
//          <myNav.MDBCollapse id="navbarCollapse3" isOpen={this.state.collapseID} navbar>
//            <myNav.MDBNavbarNav left>
//              <myNav.MDBNavItem active>
//                <myNav.MDBNavLink to="#!">Home</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBNavLink to="#!">Features</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBNavLink to="#!">Pricing</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBDropdown>
//                  <myNav.MDBDropdownToggle nav caret>
//                    <div className="d-none d-md-inline">myNav.MDBDropdown</div>
//                  </myNav.MDBDropdownToggle>
//                  <myNav.MDBDropdownMenu right>
//                    <myNav.MDBDropdownItem href="#!">Action</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Another Action</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Something else here</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Something else here</myNav.MDBDropdownItem>
//                  </myNav.MDBDropdownMenu>
//                </myNav.MDBDropdown>
//              </myNav.MDBNavItem>
//            </myNav.MDBNavbarNav>
//            <myNav.MDBNavbarNav right>
//              <myNav.MDBNavItem>
//                <myNav.MDBFormInline waves>
//                  <div className="md-form my-0">
//                    <input className="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search" />
//                  </div>
//                </myNav.MDBFormInline>
//              </myNav.MDBNavItem>
//            </myNav.MDBNavbarNav>
//          </myNav.MDBCollapse>
//        </myNav.MDBNavbar>
//        <myNav.MDBNavbar color="default-color" light expand="md" style={{ marginTop: "20px" }}>
//          <myNav.MDBNavbarBrand>
//            <strong className="white-text">myNav.MDBNavbar</strong>
//          </myNav.MDBNavbarBrand>
//          <myNav.MDBNavbarToggler onClick={this.toggleCollapse("navbarCollapse3")} />
//          <myNav.MDBCollapse id="navbarCollapse3" isOpen={this.state.collapseID} navbar>
//            <myNav.MDBNavbarNav left>
//              <myNav.MDBNavItem active>
//                <myNav.MDBNavLink to="#!">Home</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBNavLink to="#!">Features</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBNavLink to="#!">Pricing</myNav.MDBNavLink>
//              </myNav.MDBNavItem>
//              <myNav.MDBNavItem>
//                <myNav.MDBDropdown>
//                  <myNav.MDBDropdownToggle nav caret>
//                    <div className="d-none d-md-inline">myNav.MDBDropdown</div>
//                  </myNav.MDBDropdownToggle>
//                  <myNav.MDBDropdownMenu right>
//                    <myNav.MDBDropdownItem href="#!">Action</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Another Action</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Something else here</myNav.MDBDropdownItem>
//                    <myNav.MDBDropdownItem href="#!">Something else here</myNav.MDBDropdownItem>
//                  </myNav.MDBDropdownMenu>
//                </myNav.MDBDropdown>
//              </myNav.MDBNavItem>
//            </myNav.MDBNavbarNav>
//            <myNav.MDBNavbarNav right>
//              <myNav.MDBNavItem>
//                <myNav.MDBFormInline waves>
//                  <div className="md-form my-0">
//                    <input className="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search" />
//                  </div>
//                </myNav.MDBFormInline>
//              </myNav.MDBNavItem>
//            </myNav.MDBNavbarNav>
//          </myNav.MDBCollapse>
//        </myNav.MDBNavbar>
//      </myNav.MDBContainer>
//    </Router>
//    );
//  }
//}

//export default MainNavbar;