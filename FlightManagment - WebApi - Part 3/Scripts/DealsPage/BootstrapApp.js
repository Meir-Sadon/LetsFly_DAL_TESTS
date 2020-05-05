const module = angular.module("dealsApp", ["ui.router"])
    .config(['$sceDelegateProvider', function ($sceDelegateProvider) {
        // We must whitelist the JSONP endpoint that we are using to show that we trust it
        $sceDelegateProvider.resourceUrlWhitelist([
            'self',
            'https://angularjs.org/**',
            'https://geodata.solutions/**',
            'https://geodata.solutions.com',
            'https://geodata.solutions/'
        ]);
    }])