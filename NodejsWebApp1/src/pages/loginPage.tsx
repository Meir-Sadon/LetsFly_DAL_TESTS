import * as React from 'react';
//import * as lps from './../../styles/loginPageStyle.module.css';
const lps = require('./LoginPageStyle.module.css');


interface ILoginProps {
	login_submit(userName, password): void;
	forgotPassword(): void;
	newCusOrCmpReq(): void;
}

interface ILoginState {
	userName: string;
	password: string;
}

export class LoginPage extends React.Component<ILoginProps, ILoginState> {
	constructor(props: ILoginProps) {
		super(props)
		this.state = {
			userName: '',
			password: ''
		}
    }

	private handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
		switch (e.target.id) {
			case 'userNameInput':
				this.setState({
					...this.state,
					userName: e.target.value,
				})
				break;
			case 'passwordInput':
				this.setState({
					...this.state,
					password: e.target.value,
				})
				break;
			default:
				break;
        }

	}

	private forgotPassword = (e: React.SyntheticEvent<EventTarget>): void => {
		e.preventDefault();
		//this.props.login_submit(userName, password)
	}

	private newCusOrCmpReq = (e: React.SyntheticEvent<EventTarget>): void => {
		e.preventDefault();
		//this.props.login_submit(userName, password)
	}

	private loginSubmit = (e: React.SyntheticEvent<EventTarget>): void => {
		e.preventDefault();
		const { userName, password } = this.state;
		this.props.login_submit(userName, password)
	}


	public render() {
		return (

			< div className="container" style={{marginTop: '100px'}} >
				<div className="d-flex justify-content-center h-100">
					<div className={lps.user_card}>
						<div className="d-flex justify-content-center h-100">
							<div className={lps.brand_logo_container}>
								<img src="./../images/Lf_Logo.jpg" className={lps.brand_logo} alt="Logo" />
							</div>
						</div>
						<div className="d-flex justify-content-center form_container">
							<form name="loginForm" onSubmit={this.loginSubmit}>
								<div className="input-group mb-3">
									<div className="input-group-append">
										<span className={lps.input_group_text} ><i className="fas fa-user"></i></span>
									</div>
									<input type="text" name="" id="userNameInput" className={["form-control", lps.input_user].join(' ')} placeholder="username" onChange={this.handleChange}></input>
								</div>
								<div className="input-group mb-2">
									<div className="input-group-append">
										<span className={lps.input_group_text}><i className="fas fa-key"></i></span>
									</div>
									<input type="password" name="" id="passwordInput" className={["form-control", lps.input_pass].join(' ')} placeholder="password" onChange={this.handleChange}></input>
								</div>
								<div className="row form-group">
									<div className="col-xs-12" style={{ marginLeft: '20px'}}>
										<input type="checkbox" className="col-xs-2" id="customControlInline"></input>
									</div>
									<div className="col-xs-1"></div>
									<div className="col-xs-4" style={{ margin: '0px 0px 0px 10px' }}>
										<label className="col-xs-4">Remember Me</label>
									</div>
								</div>
								<div className="d-flex justify-content-center mt-3 login_container">
									<button type="submit" name="button" className={["btn", lps.login_btn].join(' ')}>Login</button>
								</div>
							</form>
						</div>
						<div className="mt-4">
							<div className="d-flex justify-content-center links" style={{ color: ' #0f0f0a' }}>
								Don't have an account? <a href="#" className="ml-2" onClick={this.newCusOrCmpReq}>Sign Up</a>
							</div>
							<div className="d-flex justify-content-center links">
								<a href="#" onClick={this.forgotPassword}>Forgot your password?</a>
							</div>
						</div>
					</div>
				</div>
			</div >
		);
	}
}

export default LoginPage

	