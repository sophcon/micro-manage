angular.module('App', ['ui.router'])
.config(['$urlRouterProvider', '$stateProvider', function ($urlRouterProvider, $stateProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider.state('List', {
        url: '/',
        templateUrl: 'List',
        controller: 'listCtrl',
        resolve: {
            product: ['$http', function ($http) {
                return $http.get('../../Home/Product').then(function (response) {
                    return response.data;
                })
            }]
        }

    })
}])