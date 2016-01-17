angular.module('App').controller('listCtrl', ['product','$scope','$http', function (product,$scope,$http) {
    $scope.products = product;
    $scope.bins = {};

    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: '../../Bins/GetBins',
        success: function (data) {
            

        }
        });
    $http.get('../../Bins/GetBins').then(function (response) {
        return $scope.bins = response.data;
    })

    console.log($scope.products);




      var con = $.hubConnection();
      var hub = con.createHubProxy("microHub");

      hub.on("productUpdate", function (message) {
          $.each($scope.products, function (i, val) {
              if (val.Id === message.ProductId) {
                  $scope.products[i].Count = message.Count;
                  $scope.$apply()
                  return false;
              }
          })
          


          return true;
          
      });
      hub.on("dispatchUpdate", function (binId, serialId) {
          console.log("binName",$scope.binName);
          if($scope.binName === binId){
              $scope.serial = serialId;
          }
          console.log(binId);
          console.log(serialId);
          $scope.$apply();
      })
      con.start(function () {
          //get initial data/object
          //hub.invoke("MethodNameOnHub");
      }).done(function () {
          //optinal in case we need to invoke method @ hub
          //hub.invoke("MethodNameOnHub");
      });

      $scope.addProduct = function () {
          console.log($scope.serial);
          console.log($scope.selected);
          $.ajax({
              url: "../../Pi/AddSerializedItem",
              type: "POST",
              data: {
                  serialId: $scope.serial,
                  productId: $scope.selected.Id,
                  binId: $scope.binName
              }
          });
      }
      


}]).directive('animateOnChange', function ($timeout) {
    return function (scope, element, attr) {
        scope.$watch(attr.animateOnChange, function (nv, ov) {
            if (nv != ov) {
                element.addClass('changed');
                $timeout(function () {
                    element.removeClass('changed');
                }, 1000);
            }
        });
    };
}).filter('yesNo', function() {
    return function(input) {
        return input ? 'yes' : 'no';
    }
})