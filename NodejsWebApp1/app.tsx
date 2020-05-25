declare var require: any

var React = require('react');
var ReactDOM = require('react-dom');
import LoginPage from './components/loginPage';
import MainNavbar from './components/MainNavbar';

export class Hello extends React.Component {
    render() {
        return (
            <div>
                <MainNavbar/>
                <h1>Welcome to React!!</h1>
                <hr />
                <LoginPage />
                <hr />
            </div>
        );
    }
}

ReactDOM.render(<Hello />, document.getElementById('root'));