import * as React from 'react';
const lps = require('./loginPageStyle.module.css');


interface IProps {
	login_submit(userName, password): void;
}

interface IState {
	userName: string;
	password: string;
}

export class LoginPage extends React.Component<IProps, IState> {
	state: IState = {
		userName: '',
		password: ''
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
							<div className="d-flex justify-content-center links">
								Don't have an account? <a href="#" className="ml-2">Sign Up</a>
							</div>
							<div className="d-flex justify-content-center links">
								<a href="#">Forgot your password?</a>
							</div>
						</div>
					</div>
				</div>
			</div >
		);
	}
}

export default LoginPage



			//<div className="container">
			//	<div className="d-flex justify-content-center h-100">
			//		<div className="user-card">
			//			<div className="d-flex justify-content-center h-100">
			//				<div className="brand-logo-container">
			//					<img src="./../Images/Lf_Logo.jpg" className="brand-logo" alt="Logo" />
			//				</div>
			//			</div>
			//			<div className="d-flex justify-content-center form-container">
			//				<form>
			//					<div className="input-group mb-3">
			//						<div className="input-group-append">
			//							<span className="input-group-text" ><i className="fas fa-user"></i></span>
			//						</div>
			//						<input type="text" name="" className="form-control lps.input-user" placeholder="username"></input>
			//					</div>
			//					<div className="input-group mb-2">
			//						<div className="input-group-append">
			//							<span className="input-group-text"><i className="fas fa-key"></i></span>
			//						</div>
			//						<input type="password" name="" className="form-control input-pass" placeholder="password"></input>
			//					</div>
			//					<div className="form-group">
			//						<div className="custom-control custom-checkbox">
			//							<input type="checkbox" className="custom-control-input" id="customControlInline"></input>
			//							<label className="custom-control-label">Remember me</label>
			//						</div>
			//					</div>
			//					<div className="d-flex justify-content-center mt-3 login-container">
			//						<button type="button" name="button" className="btn login-btn">Login</button>
			//					</div>
			//				</form>
			//			</div>
			//			<div className="mt-4">
			//				<div className="d-flex justify-content-center links">
			//					Don't have an account? <a href="#" className="ml-2">Sign Up</a>
			//				</div>
			//				<div className="d-flex justify-content-center links">
			//					<a href="#">Forgot your password?</a>
			//				</div>
			//			</div>
			//		</div>
			//	</div>
			//</div>


	