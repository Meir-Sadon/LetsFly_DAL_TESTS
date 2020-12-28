"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginForm = void 0;
const React = require("react");
const lps = require('./../styles/loginFormStyle');
const formik_1 = require("formik");
//export class LoginPage extends React.Component<null, IMyFormFields> {
exports.LoginForm = () => {
    const initialValues = { userName: '', password: '' };
    const forgotPassword = () => {
    };
    const newCusOrCmpReq = () => {
    };
    return (React.createElement("div", { className: "container", style: { marginTop: '100px' } },
        React.createElement("div", { className: "d-flex justify-content-center h-100" },
            React.createElement("div", { className: lps.user_card },
                React.createElement("div", { className: "d-flex justify-content-center h-100" },
                    React.createElement("div", { className: lps.brand_logo_container },
                        React.createElement("img", { src: "./../images/Lf_Logo.jpg", className: lps.brand_logo, alt: "Logo" }))),
                React.createElement("div", { className: "d-flex justify-content-center form_container" },
                    React.createElement(formik_1.Formik, { initialValues: initialValues, onSubmit: (values, actions) => {
                            console.log({ values, actions });
                            //alert(JSON.stringify(values, null, 2));
                            actions.setSubmitting(false);
                        } }, () => (React.createElement(formik_1.Form, { name: "loginForm" },
                        React.createElement("div", { className: "input-group mb-3" },
                            React.createElement("div", { className: "input-group-append" },
                                React.createElement("span", { className: lps.input_group_text },
                                    React.createElement("i", { className: "fas fa-user" }))),
                            React.createElement(formik_1.Field, { type: "text", name: "username", id: "userNameInput", className: ["form-control", lps.input_user].join(' '), placeholder: "username" })),
                        React.createElement("div", { className: "input-group mb-2" },
                            React.createElement("div", { className: "input-group-append" },
                                React.createElement("span", { className: lps.input_group_text },
                                    React.createElement("i", { className: "fas fa-key" }))),
                            React.createElement(formik_1.Field, { type: "password", name: "password", id: "passwordInput", className: ["form-control", lps.input_pass].join(' '), placeholder: "password..." })),
                        React.createElement("div", { className: "row form-group" },
                            React.createElement("div", { className: "col-xs-12", style: { marginLeft: '20px' } },
                                React.createElement("input", { type: "checkbox", className: "col-xs-2", id: "customControlInline" })),
                            React.createElement("div", { className: "col-xs-1" }),
                            React.createElement("div", { className: "col-xs-4", style: { margin: '0px 0px 0px 10px' } },
                                React.createElement("label", { className: "col-xs-4" }, "Remember Me"))),
                        React.createElement("div", { className: "d-flex justify-content-center mt-3 login_container" },
                            React.createElement("button", { type: "submit", name: "button", className: ["btn", lps.login_btn].join(' ') }, "Login")))))),
                React.createElement("div", { className: "mt-4" },
                    React.createElement("div", { className: "d-flex justify-content-center links", style: { color: ' #0f0f0a' } },
                        "Don't have an account? ",
                        React.createElement("a", { href: "#", className: "ml-2", onClick: newCusOrCmpReq }, "Sign Up")),
                    React.createElement("div", { className: "d-flex justify-content-center links" },
                        React.createElement("a", { href: "#", onClick: forgotPassword }, "Forgot your password?")))))));
};
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
//# sourceMappingURL=LoginForm.js.map