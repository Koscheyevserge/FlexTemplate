import React from 'react'

import { Tag } from './Tag.jsx'
import { TagForm } from './TagForm.jsx'
export class TagList extends React.Component {
    constructor(props) {
        super(props);
        this.state = { tags: [] };
        this.onAddTag = this.onAddTag.bind(this);
        //this.onRemoveTag = this.onRemoveTag.bind(this);
    }
    onAddTag(tag) {
        if (tag) {
            //TODO - сделать добавление тега на клиенте, не посылать запрос к серверу
            var data = JSON.stringify({"name":tag.name});
            var xhr = new XMLHttpRequest();
            var origin = window.location.origin;
            xhr.open("post", origin + "/Admin/Create", true);
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    loadData() {
        var xhr = new XMLHttpRequest();
        var origin = window.location.origin;
        xhr.open("get", origin + "/Admin/Get", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ tags: data });
            var test = this.state.tags;
            console.log(test);
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    render() {
        //var remove = this.onRemovePhone;
        return <div>
            <h2>Создать тег</h2>
            <TagForm onTagSubmit={this.onAddTag} />
            <h2>Список тегов</h2>
            <div>
                {
                    this.state.tags.map(function (tag) {
                        return <Tag tag={tag}/>;
                    })
                }
            </div>
        </div>;
    }
}