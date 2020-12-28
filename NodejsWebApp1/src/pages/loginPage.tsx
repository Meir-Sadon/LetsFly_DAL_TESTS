import * as React from 'react';
const lps = require('../style/loginStyleOld.css');
import { Formik, Form, Field } from 'formik';

interface MyFormFields {
	userName: string,
	password: string;
}

//export class LoginPage extends React.Component<null, IMyFormFields> {
export const LoginPage: React.FC<{}> = () => {

	const initialValues: MyFormFields = { userName: '', password: '' };
	const forgotPassword = () => {

	}
	const newCusOrCmpReq = () => {

    }

	return (

< div className="container" style={{ marginTop: '100px' }} >
	<div className="d-flex justify-content-center h-100">
		<div className={lps.user_card}>
			<div className="d-flex justify-content-center h-100">
				<div className={lps.brand_logo_container}>
					<img src="./../images/Lf_Logo.jpg" className={lps.brand_logo} alt="Logo" />
				</div>
			</div>
			<div className="d-flex justify-content-center form_container">
				<Formik
					initialValues={initialValues}
					onSubmit={(values, actions) => {
						console.log({ values, actions });
						alert(JSON.stringify(values, null, 2));
						actions.setSubmitting(false);
					}}
				>
					{
						() => (
							<Form name="loginForm">
								<div className="input-group mb-3">
									<div className="input-group-append">
										<span className={lps.input_group_text} ><i className="fas fa-user"></i></span>
									</div>
									<Field type="text" name="username" id="userNameInput" className={["form-control", lps.input_user].join(' ')} placeholder="username"></Field>
								</div>
								<div className="input-group mb-2">
									<div className="input-group-append">
										<span className={lps.input_group_text}><i className="fas fa-key"></i></span>
									</div>
									<Field type="password" name="password" id="passwordInput" className={["form-control", lps.input_pass].join(' ')} placeholder="password..."></Field>
								</div>
								<div className="row form-group">
									<div className="col-xs-12" style={{ marginLeft: '20px' }}>
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
							</Form>
						)}
				</Formik>
			</div>
			<div className="mt-4">
				<div className="d-flex justify-content-center links" style={{ color: ' #0f0f0a' }}>
					Don't have an account? <a href="#" className="ml-2" onClick={newCusOrCmpReq}>Sign Up</a>
				</div>
				<div className="d-flex justify-content-center links">
					<a href="#" onClick={forgotPassword}>Forgot your password?</a>
				</div>
			</div>
		</div>
	</div>
</div >

	);
}


// All Login Page Without Module Css
//< div className="container" style={{ marginTop: '100px' }} >
//	<div className="d-flex justify-content-center h-100">
//		<div className="user_card">
//			<div className="d-flex justify-content-center h-100">
//				<div className="brand_logo_container">
//					<img src="./../images/Lf_Logo.jpg" className="brand_logo" alt="Logo" />
//				</div>
//			</div>
//			<div className="d-flex justify-content-center form_container">
//				<Formik
//					initialValues={initialValues}
//					onSubmit={(values, actions) => {
//						console.log({ values, actions });
//						alert(JSON.stringify(values, null, 2));
//						actions.setSubmitting(false);
//					}}
//				>
//					{
//						() => (
//							<Form name="loginForm">
//								<div className="input-group mb-3">
//									<div className="input-group-append">
//										<span className="input_group_text" ><i className="fas fa-user"></i></span>
//									</div>
//									<Field type="text" name="username" id="userNameInput" className="form-control input_user" placeholder="username"></Field>
//								</div>
//								<div className="input-group mb-2">
//									<div className="input-group-append">
//										<span className="input_group_text"><i className="fas fa-key"></i></span>
//									</div>
//									<Field type="password" name="password" id="passwordInput" className="form-control lps.input_pass" placeholder="password..."></Field>
//								</div>
//								<div className="row form-group">
//									<div className="col-xs-12" style={{ marginLeft: '20px' }}>
//										<input type="checkbox" className="col-xs-2" id="customControlInline"></input>
//									</div>
//									<div className="col-xs-1"></div>
//									<div className="col-xs-4" style={{ margin: '0px 0px 0px 10px' }}>
//										<label className="col-xs-4">Remember Me</label>
//									</div>
//								</div>
//								<div className="d-flex justify-content-center mt-3 login_container">
//									<button type="submit" name="button" className="btn login_btn">Login</button>
//								</div>
//							</Form>
//						)}
//				</Formik>
//			</div>
//			<div className="mt-4">
//				<div className="d-flex justify-content-center links" style={{ color: ' #0f0f0a' }}>
//					Don't have an account? <a href="#" className="ml-2" onClick={newCusOrCmpReq}>Sign Up</a>
//				</div>
//				<div className="d-flex justify-content-center links">
//					<a href="#" onClick={forgotPassword}>Forgot your password?</a>
//				</div>
//			</div>
//		</div>
//	</div>
//</div >







	//private handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
	//	switch (e.target.id) {
	//		case 'userNameInput':
	//			this.setState({
	//				...this.state,
	//				userName: e.target.value,
	//			})
	//			break;
	//		case 'passwordInput':
	//			this.setState({
	//				...this.state,
	//				password: e.target.value,
	//			})
	//			break;
	//		default:
	//			break;
 //       }

	//}

	//private forgotPassword = (e: React.SyntheticEvent<EventTarget>): void => {
	//	e.preventDefault();
	//	//this.props.login_submit(userName, password)
	//}

	//private newCusOrCmpReq = (e: React.SyntheticEvent<EventTarget>): void => {
	//	e.preventDefault();
	//}

	//private loginSubmit = (e: React.SyntheticEvent<EventTarget>): void => {
	//	e.preventDefault();
	//	const { userName, password } = this.state;
	//}


	//public render() {
	//	
	//}


//export default LoginPage

	