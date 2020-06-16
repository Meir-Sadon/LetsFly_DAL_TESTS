//const isDevelopment = process.env.NODE_ENV === 'development'
//const MiniCssExtractPlugin = require('mini-css-extract-plugin')

module.exports = {
    devtool: 'source-map',
    entry: "index.tsx",
    mode: "development",
    output: {
        filename: "./app-bundle.js"
    },
    resolve: {
        extensions: ['.Webpack.js', '.web.js', '.ts', '.js', '.jsx', '.tsx']
    },
    //plugins: [
    //     new MiniCssExtractPlugin({
    //     filename: isDevelopment ? '[name].css' : '[name].[hash].css',
    //     chunkFilename: isDevelopment ? '[id].css' : '[id].[hash].css'
    //     })
    //],
    module: {
        rules: [
            {
                test: /\.tsx$/,
                use: {
                    loader: 'ts-loader'
                },
                exclude: /(node_modules|bower_components)/
            },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1,
                            modules: true
                        }
                    }
                ],
                include: /\.module\.css$/
            },
           {
                test: /\.css$/,
                use: [
                'style-loader',
                'css-loader'
                ],
                exclude: /\.module\.css$/
           },
           {
                test: /\.(png|jpe?g|gif)$/i,
                use: [{
                    loader: 'file-loader',
                },],
            }
        ]
    },
}


//{
            //    test: /\.module\.s(a|c)ss$/,
            //    loader: [
            //         isDevelopment ? 'style-loader' : MiniCssExtractPlugin.loader,
            //         {
            //            loader: 'css-loader',
            //            options: {
            //                modules: true,
            //                sourceMap: isDevelopment
            //            }
            //        },
            //        {
            //            loader: 'sass-loader',
            //            options: {
            //                sourceMap: isDevelopment
            //            }
            //        }
            //    ]
            //},
            //{
            //    test: /\.s(a|c)ss$/,
            //    exclude: /\.module.(s(a|c)ss)$/,
            //   loader: [
            //         isDevelopment ? 'style-loader' : MiniCssExtractPlugin.loader,
            //         'css-loader',
            //         {
            //    loader: 'sass-loader',
            //           options: {
            //                sourceMap: isDevelopment
            //            }
            //          }
            //        ]
            //      },