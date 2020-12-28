module.exports = {
    devtool: 'source-map',
    entry: "index.jsx",
    mode: "development",
    output: {
        filename: "./app-bundle.js"
    },
    resolve: {
        extensions: ['.Webpack.js', '.web.js', '.ts', '.js', '.jsx', '.tsx']
    },
    module: {
        rules: [
            {
                test: /\.tsx$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'ts-loader'
                }
            }
        ]
    }
}

//module.exports = {
//    devtool: 'source-map',
//    entry: "index.tsx",
//    mode: "development",
//    output: {
//        filename: "./app-bundle.js"
//    },
//    resolve: {
//        extensions: ['.Webpack.js', '.web.js', '.ts', '.js', '.jsx', '.tsx']
//    },
//    module: {
//        rules: [
//            {
//                test: /\.tsx$/,
//                use: {
//                    loader: 'ts-loader'
//                },
//                exclude: /(node_modules|bower_components)/
//            }//,
//            {
//                test: /\.css$/,
//                use: [
//                    'style-loader',
//                    {
//                        loader: 'css-loader',
//                        options: {
//                            importLoaders: 1,
//                            modules: true
//                        }
//                    }
//                ],
//                include: /\.module\.css$/
//            },
//            {
//                test: /\.css$/,
//                use: [
//                    'style-loader',
//                    'css-loader'
//                ],
//                exclude: /\.module\.css$/
//            },
//            {
//                test: /\.(png|jpe?g|gif)$/i,
//                use: [{
//                    loader: 'file-loader',
//                },],
//            }
//        ]
//    },
//}