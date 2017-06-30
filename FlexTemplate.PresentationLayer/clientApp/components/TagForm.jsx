import React from 'react';

export class TagForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {name: ""}
        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
    }
    onNameChange (e) {
        this.setState ({name: e.target.value})
    }
    onSubmit (e) {
        e.preventDefault()
        var tagName = this.state.name.trim();
        if (!tagName) {
            return;
        }
        this.setState ({name: ""})
    }
    render() {
        console.log(this.state);
        return <form onSubmit={this.onSubmit}>
            <p>
                <input type="text"
                       placeholder="NAME"
                       value={this.state.name}
                       onChange={this.onNameChange}/>
            </p>
            <input type="submit" value="Сохранить"/>
        </form>
    }
}