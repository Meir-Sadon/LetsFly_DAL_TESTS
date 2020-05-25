var React = require('react');
const lps = require('./../Content/LoginPageStyle.module.css');

class LoginPage extends React.Component {
    public render() {
        return (
	<div className={lps.container}>
		<div className="d-flex justify-content-center h-100">
			<div className={lps.user_card}>
				<div className="d-flex justify-content-center h-100">
					<div className={lps.brand_logo_container}>
						<img src="./../Content/Images/Lf_Logo.jpg" className={lps.brand_logo} alt="Logo"/>
					</div>
				</div>
				<div className="d-flex justify-content-center form_container">
					<form>
						<div className="input-group mb-3">
							<div className="input-group-append">
								<span className={lps.input_group_text} ><i className="fas fa-user"></i></span>
							</div>
							<input type="text" name="" className={["form-control", lps.input_user].join(' ')} placeholder="username"></input>
						</div>
						<div className="input-group mb-2">
						    <div className="input-group-append">
						        <span className={lps.input_group_text}><i className="fas fa-key"></i></span>
						    </div>
						    <input type="password" name="" className={["form-control", lps.input_pass].join(' ')} placeholder="password"></input>
						</div>
						<div className="form-group">
						    <div className="custom-control custom-checkbox">
						        <input type="checkbox" className="custom-control-input" id="customControlInline"></input>
						        <label className="custom-control-label">Remember me</label>
						    </div>
						</div>
						<div className="d-flex justify-content-center mt-3 login_container">
				 			<button type="button" name="button" className={["btn", lps.login_btn].join(' ')}>Login</button>
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
	</div>	
        );
    }
}

export default LoginPage