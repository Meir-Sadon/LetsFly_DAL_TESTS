import { App as Component } from './App'
import { connect } from 'react-redux';
import { Dispatch } from 'react';
import { SetUserType, IAction } from './actions';
import { IAppState, store} from './store'
import { compose } from 'redux';
import { UserTypes } from './userTypes';

import axios from 'axios';
import swal from 'sweetalert2';

const mapStateToProps = (state: IAppState) => {
    return {
        userType: state.app.userType
    };
};

const mapDispatchToProps = (dispatch: Dispatch<IAction>) => {
    return {
        trySubmit: (userName, password) => {
            axios.post("api/Auth", {
                User_Name: userName,
                Password: password
            })
                .then((res) => {
                    switch (JSON.parse(res.data).type) {
                        case "Administrator":
                            dispatch(SetUserType(UserTypes.TYPE_1))
                            break;
                        case "Airline":
                            dispatch(SetUserType(UserTypes.TYPE_2))
                            break;
                        case "Customer":
                            dispatch(SetUserType(UserTypes.TYPE_3))
                            break;
                        default:
                            dispatch(SetUserType(UserTypes.TYPE_4))
                    }                    
                    swal.fire("Successfully Connected", `You Will Immadiately Be Taken To The Requested Page.`, 'success')
                }, (err) => {
                    console.log(err.response);
                    swal.fire(`Login Failed`, `${err.response.data}`, "error")
                });
        },
        askWhichUserToRegister: () => alert("hello"),
        handleRestorePassword: () => alert("Hello Again")
    };
};

export const App = compose(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(Component);