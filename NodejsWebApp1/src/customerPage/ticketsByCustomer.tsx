
import * as React from 'react';
import { connect } from 'react-redux';


interface ITicketsByCustomerProps {
}

interface ITicketsByCustomerState {
    userName: string;
}


class TicketsByCustomer extends React.Component<ITicketsByCustomerProps, ITicketsByCustomerState> {
    constructor(props: ITicketsByCustomerProps) {
        super(props)
        this.state = {
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
