angular.module('App').controller('listCtrl', ['product','$scope','$http', function (product,$scope,$http) {
    $scope.products = product;

    
}])