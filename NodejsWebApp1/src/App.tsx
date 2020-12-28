
import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import MainNavbar from './../src/mainNavbar';
import { LoginPage } from './pages/loginPage';


import { UserTypes } from './userTypes';

const bgStyles = {
    height: '880px',
    backgroundImage: "url('./../images/loginBackground.jpg')",
    padding: '10px',
    backgroundSize: 'cover'
}

interface IAppProps {
    userType: UserTypes;
    identity: UserTypes;
    //trySubmit(userName, password): void;
    //askWhichUserToRegister(): void;
    //handleRestorePassword(): void;
}

export const App: React.FunctionComponent<IAppProps> = ( props ) => (
    <div style={bgStyles} className="stretchy">
        <Router>
            <Route render={() => <MainNavbar userType={props.userType} />}/>
            <Switch>
                <Route path={["/login-c"]} render={() => <LoginPage/>} />
            </Switch>
        </Router>
    </div>
);

