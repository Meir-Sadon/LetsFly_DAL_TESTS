declare var require: any

import * as React from 'react';
import { Link } from 'react-router-dom';

import { UserTypes } from './userTypes';
const MainNav = require('react-bootstrap');
//import 'bootstrap/dist/css/bootstrap.css';


interface IMainProps {
    userType: any
}

const MainNavbar: React.FunctionComponent<IMainProps> = ({userType}) => (
            <div style={{ padding: '2px' }}>
                <MainNav.Navbar collapseOnSelect expand="lg" className="navbar_inverse">
                    <MainNav.Navbar.Brand><Link to="/home"></Link>LET'S FLY</MainNav.Navbar.Brand>
                    <MainNav.Navbar.Toggle aria-controls="responsive-navbar-nav" style={{backgroundImage: 'url(./../images/hamburger_icon.jpg)', height: '30px', width: '30px', backgroundSize: '35px', backgroundPosition: 'center'}}/>
                    <MainNav.Navbar.Collapse id="responsive-navbar-nav">
                <MainNav.Nav className="ml-auto">


                    <div style={{ display: userType == UserTypes.TYPE_1 ? 'flex' : 'none' }}>
                        <MainNav.NavDropdown title="Management" id="collasible-nav-dropdown">
                            <Link className="dropdown-item" to="/edit-companies">Edit Airlines</Link>
                            <Link className="dropdown-item" to="/edit-customers">Edit Customers</Link>
                        </MainNav.NavDropdown>
                        <Link className="nav-link nav-link" to="/admin-inbox">Inbox</Link>
                        <Link className="nav-link nav-link" to="/search-flights">Search Flights</Link>
                        <Link className="nav-link nav-link" to="/admin-profile">Profile</Link>
                        <Link className="nav-link nav-link" to="/home">Log out</Link>
                    </div>


                    <div style={{ display: userType == UserTypes.TYPE_2 ? 'flex' : 'none' }}>
                        <Link className="nav-link nav-link" to="/own-flights">Own Flights</Link>
                        <Link className="nav-link nav-link" to="/company-inbox">Inbox</Link>
                        <Link className="nav-link nav-link" to="/search-flights">Search Flights</Link>
                        <Link className="nav-link nav-link" to="/company-profile">Profile</Link>
                        <Link className="nav-link nav-link" to="/home">Log out</Link>
                    </div>


                    <div style={{ display: userType == UserTypes.TYPE_3 ? 'flex' : 'none' }}>
                        <MainNav.NavDropdown title="Manage My Tickets" id="collasible-nav-dropdown">
                            <Link className="dropdown-item" to="/buy-ticket">Buy New Ticket</Link>
                            <Link className="dropdown-item" to="/edit-tickets">Show/Edit/Remove</Link>
                        </MainNav.NavDropdown>
                        <Link className="nav-link nav-link" to="/customer-inbox">Inbox</Link>
                        <Link className="nav-link nav-link" to="/search-flights">Search Flights</Link>
                        <Link className="nav-link nav-link" to="/customer-profile">Profile</Link>
                        <Link className="nav-link nav-link" to="/home">Log out</Link>
                    </div>


                    <div style={{ display: userType == UserTypes.TYPE_4 ? 'flex' : 'none' }}>
                        <MainNav.NavDropdown title="Sign-Up" id="collasible-nav-dropdown">
                            <Link className="dropdown-item" to="/page/customerregister">As Customer</Link>
                            <Link className="dropdown-item" to="/page/companyregister">As Company</Link>
                        </MainNav.NavDropdown>
                        <MainNav.NavDropdown title="Sign-In" id="collasible-nav-dropdown">
                            <Link className="dropdown-item" to="/logincustomer">As Customer</Link>
                            <Link className="dropdown-item" to="/logincompany">As Company</Link>
                            <MainNav.NavDropdown.Divider />
                            <Link className="dropdown-item" to="/adminlogin">As Administrator</Link>
                        </MainNav.NavDropdown>
                        <Link className="nav-link nav-link" to="/page/searchflight">Search Flights</Link>
                    </div>
                    </MainNav.Nav>
                </MainNav.Navbar.Collapse>
                </MainNav.Navbar>      
            </div>
);

export default MainNavbar;