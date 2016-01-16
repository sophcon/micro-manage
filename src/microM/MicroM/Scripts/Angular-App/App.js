﻿angular.module('App', ['ui.router'])
.config(['$urlRouterProvider', '$stateProvider', function ($urlRouterProvider, $stateProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider.state('List', {
        url: '/',
        templateUrl: 'List',
        controller: 'listCtrl'
        //resolve: {
        //    products: ['$http', function ($http) {
        //        return $h
        //    }]
        //}
    })
}])