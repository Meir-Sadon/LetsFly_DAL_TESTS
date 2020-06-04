import { App as Component } from './App'
import { connect } from 'react-redux';
import { Dispatch } from 'react';
import { SetAge, IAction } from './actions';
import { IAppState} from './store'
import { compose } from 'redux';

const mapStateToProps = (state: IAppState) => {
    return {
        age: state.app.age
    };
};

const mapDispatchToProps = (dispatch: Dispatch<IAction>) => {
    return {
        onClick: () => dispatch(SetAge(Math.floor(Math.random() * 100)))
    };
};

export const App = compose(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(Component);