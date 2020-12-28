import { App as Component } from '../components/App'
//import { Dispatch } from 'react';
//import { compose } from 'react-redux';
import { connect } from 'react-redux';
import { SetUserType, SetRegIdentity, IAction } from '../redux/actions/actions';
import { IAppState, store } from '../redux/store'
import { UserTypes } from '../enums/userTypes';
import axios from 'axios';
import swal from 'sweetalert2';

export default function trySubmit(userName, password) {
    axios.post("https://localhost:951/api/Auth", {
        User_Name: userName,
        Password: password
    })
        .then((res) => {
            console.log(res)
            switch (JSON.parse(res.data).type) {
                case UserTypes.TYPE_1:
                    SetUserType(UserTypes.TYPE_1)
                    break;
                case UserTypes.TYPE_2:
                    SetUserType(UserTypes.TYPE_2)
                    break;
                case UserTypes.TYPE_3:
                    SetUserType(UserTypes.TYPE_3)
                    break;
                default:
                    SetUserType(UserTypes.TYPE_4)
            }
            swal.fire("Successfully Connected", `You Will Immadiately Be Taken To The Requested Page.`, 'success')
        }, (err) => {
            console.log(err.response);
            swal.fire(`Login Failed`, `${err.response.data}`, "error")
        });
}

const askWhichUserToRegister = () => {
    swal.fire({
        title: 'Choose Identity',
        icon: 'question',
        html: getHtmlForChooseIdentityMessage(),
        showCancelButton: false,
        showConfirmButton: false,
        onClose: onIdentitySwalClosed
    });

    function onIdentitySwalClosed(): void {
        console.log(store.getState().app.identity)
        switch (store.getState().app.identity) {
            case UserTypes.TYPE_2:
                axios.post("https://localhost:951/homepage/cusreg");
                break;
            case UserTypes.TYPE_3:
                axios.post("https://localhost:951/homepage/cmpreg");
                break;
            default:
        }
    }

    $("#btnRegCst")!.click(function () {// The '!' Before click, Tell To Complier That Is Not Null
        SetRegIdentity(UserTypes.TYPE_3)
    });
    $("#btnRegCmp")!.click(function () {// The '!' Before click, Tell To Complier That Is Not Null
        SetRegIdentity(UserTypes.TYPE_2)
    });
}

const handleRestorePassword = () => { console.log("Im From Handle Restore Password") }

function getHtmlForChooseIdentityMessage(): string {
    return 'Are You Want To Register As Customer Or Company?' + "<br>" + "<br>" +
        `<a href="https://localhost:951/#/customerregister"><button type="button" id="btnRegCst" role="button" tabindex="0" class="btn-primary m-5" style="height:30px" >` + `As Customer` + `</button> </a>` +
        `<a href="https://localhost:951/#/companyregister"><button type="button" id="btnRegCmp" role="button" tabindex="0" class="btn-primary m-5" style="height:30px" >` + 'As Company' + '</button> </a>'
}

export const App = connect(
    (state: IAppState) => ({
        userType: state.app.userType,
        identity: state.app.identity
    }),
    {
        SetUserType, SetRegIdentity
    }
)(Component);