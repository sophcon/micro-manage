angular.module('App').controller('listCtrl', ['product','$scope','$http', function (product,$scope,$http) {
    $scope.products = product;


    console.log($scope.products);
    $scope.send = function () {

       
        console.log($scope.message);
        $.ajax({
            url: "../../Home/SignalRTest",
            type: "POST",
            data: {message:$scope.message},
            success: function (data) {
                console.log("message sent: ", data);
            },
            error: function () {
                console.log("error happened sending test");
            }
        });
}



      var con = $.hubConnection();
      var hub = con.createHubProxy("microHub");

      hub.on("productUpdate", function (message) {
          console.log(message);
          
      });

      con.start(function () {
          //get initial data/object
          //hub.invoke("MethodNameOnHub");
      }).done(function () {
          //optinal in case we need to invoke method @ hub
          //hub.invoke("MethodNameOnHub");
      });


    


}])