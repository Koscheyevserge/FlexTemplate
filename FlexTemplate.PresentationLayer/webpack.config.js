"use strict";
{
    var webpack = require('webpack'); 

    var bundleFolder = 'wwwroot/js/';
    var path = require("path");
    module.exports = {
        entry: [
            "webpack-dev-server/client?http://127.0.0.1:8080",
            "webpack/hot/only-dev-server",
            "./clientApp/boot.jsx"
        ],
        output: {
            filename: "devBundle.js",
            path: path.resolve(__dirname, bundleFolder),
            publicPath: 'http://127.0.0.1:8080/'
        },
        module: {
            loaders: [
                {
                    test: /\.(js|jsx)$/,
                    loaders: ['react-hot-loader', 'babel-loader', 'babel-loader?presets[]=es2015&presets[]=react'],
                    exclude: /node_modules/
                }
            ]
        },
        devtool: "source-map",
        
        resolve: {
            extensions: ["*", ".webpack.js", ".web.js", ".js", ".jsx"]
        },
        devServer: {
            inline:true,
            hot: true,
            headers: { "Access-Control-Allow-Origin": "*" }
        }
    };
}
