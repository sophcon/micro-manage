angular.module('App').controller('listCtrl', ['product','$scope','$http', function (product,$scope,$http) {
    $scope.products = product;


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
              url: "../../Pi/AddInventory",
              type: "POST",
              data: {
                  serialId: $scope.serial,
                  productId: $scope.selected,
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