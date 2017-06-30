import React from 'react'
import { TagList } from './components/TagList.jsx'
import { BrowserRouter, Route } from 'react-router-dom'

export class App extends React.Component {
    constructor(props) {
        super(props);
    }
    render () {
        console.log(BrowserRouter);
        console.log(Route);
        return <BrowserRouter>
                <Route path="/">
                    <Route path='/admin' component={TagList}>
                        <Route path='/tags' component={TagList} />
                    </Route>
                </Route>
        </BrowserRouter>
    }
/*
    render () {
        return <TagList />
    }*/
}


