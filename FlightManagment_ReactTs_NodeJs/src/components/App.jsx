import * as React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import MainNavbar from './MainNavbar';
import { LoginForm } from './LoginForm';


import { UserTypes } from './../enums/userTypes';

const bgStyles = {
    height: '850px',
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

export const App: React.FunctionComponent<IAppProps> = (props) => (
    <div style={bgStyles} className="stretchy">
        <button className="btn btn-primary">Click Me</button>
        <Router>
            <Route render={() => <MainNavbar userType={props.userType} />} />
            <Switch>
                <Route path={["/login-c"]} render={() => <LoginForm />} />
            </Switch>
        </Router>
    </div>
);

