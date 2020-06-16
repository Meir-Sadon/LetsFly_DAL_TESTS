import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import { MainNavbar } from './../src/mainNavbar';
import LoginPage from './../src/pages/loginPage';
import 'bootstrap/dist/css/bootstrap.css';

import axios from 'axios';
import swal from 'sweetalert2';

const bgStyles = {
    height: '800px',
    backgroundImage: "url('./../images/loginBackground.jpg')",
    padding: '10px'
}

interface IAppProps {
    userType: string;
    trySubmit(userName, password): void;
    askWhichUserToRegister(): void;
    handleRestorePassword(): void;
}

export const App: React.FunctionComponent<IAppProps> = ({ userType, trySubmit, askWhichUserToRegister, handleRestorePassword  }) => (
    <div style={bgStyles} className="stretchy">
        <Router>
            <MainNavbar userType={userType} />
            <Switch>
                <Route exact path={["/log-as-customer", "/log-as-company"]} render={() => <LoginPage login_submit={trySubmit} newCusOrCmpReq={askWhichUserToRegister} forgotPassword={handleRestorePassword} />}/>
            </Switch>
        </Router>
    </div>
);

