const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const WebpackCleanupPlugin = require("webpack-cleanup-plugin");


module.exports = {
  context: __dirname,
  entry: {
    main: `./index.js`,
  },

  output: {
    filename: '[name].js',
    path :path.resolve(__dirname,"./dist")
  },
  plugins: [
    new ExtractTextPlugin('style.css'),
    new WebpackCleanupPlugin()
  ],
  resolve: {
    extensions: ['.js', '.ts', '.scss', '.css'],
  },
  module: {
    loaders: [
      {
        test: /\.sass$/,
        use: ExtractTextPlugin.extract({
          fallback: 'style-loader',
          //resolve-url-loader may be chained before sass-loader if necessary
          use: ['css-loader', 'sass-loader']
        })
      }
    ],
  },
};
