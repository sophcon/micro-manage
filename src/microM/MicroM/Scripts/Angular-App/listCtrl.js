angular.module('App').controller('listCtrl', ['product','$scope','$http', function (product,$scope,$http) {
    $scope.product = product;
    console.log($scope.product);
}])