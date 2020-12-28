import * as React from 'react';
import { connect } from 'react-redux';


interface ITicketsByCustProps {
}

interface ITicketsByCustState {
    userName: string;
}


class TicketsByCust extends React.Component<ITicketsByCustProps, ITicketsByCustState> {
    constructor(props: ITicketsByCustProps) {
        super(props)
        const state = {
            userName: ''
        }
    }

    public render() {
        return (
            <div>
            </div>
        );
    }
}
