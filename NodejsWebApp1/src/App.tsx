import React from 'react';
import MainNavbar from './../components/mainNavbar';
import LoginPage from './../components/loginPage';

function trySubmit(userName, password) {
    alert(userName);
    alert(password);
};

export const App: React.FunctionComponent = () => {
    return <div style={{ height: '800px', backgroundImage: "url('./../images/loginBackground.jpg')", WebkitBackgroundSize: 'cover', MozBackgroundSize: 'cover', OBackgroundSize: 'cover', backgroundSize: 'cover',padding: '10px'}}>
        <MainNavbar />
        <LoginPage login_submit={trySubmit} />
    </div>;
}