import React from 'react';

export class Tag extends React.Component {
    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        console.log("это событие нажатия на кнопку Удалить в коипоненте Tag n/" );
        console.log(e);
        //this.props.onRemove(this.state.data);
    }
    render() {
        console.log(this.state);
        return <div>
            <p>Тег: <b>{this.props.tag.name}</b></p>
            <p><button onClick={this.onClick}>Удалить</button></p>
        </div>;
    }
}