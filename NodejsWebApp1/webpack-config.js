module.exports = {
    devtool: 'source-map',
    entry: "./app.tsx",
    mode: "development",
    output: {
        filename: "./app-bundle.js", devtoolModuleFilenameTemplate: '[resource-path]'  // removes the webpack:/// prefix 
    },
    resolve: {
        extensions: ['.Webpack.js', '.web.js', '.ts', '.js', '.jsx', '.tsx']
    },
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
        use: [
          {
            loader: 'file-loader',
          },
        ],
      }
        ]
    },
}
    //plugins: [
    //  new webpack.WatchIgnorePlugin([/css\.d\.ts$/])
    //]
    //test: /\.css$/,
    //use: [
    //  require.resolve('style-loader'),
    //  {
    //    loader: require.resolve('typings-for-css-modules-loader'),
    //   options: {
    //      modules: true,
    //      importLoaders: 1,
    //      localIdentName: '[name]__[local]___[hash:base64:5]',
    //     namedExport: true,
    //      camelCase: true
    //    },
    //  },