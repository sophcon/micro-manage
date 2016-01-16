angular.module('starter.controllers', [])
  .controller('DashCtrl', function($scope, $cordovaNfc, $cordovaNfcUtil) {

  var deploy = new Ionic.Deploy();
  $scope.testMsg = "NOTHING YET";

  $cordovaNfc.then(function(nfcInstance){

        //Use the plugins interface as you go, in a more "angular" way
      nfcInstance.addNdefListener(function(event){
            //Callback when ndef got triggered
      })
      .then(
        //Success callback
        function(event){
            console.log("bound success");
            $scope.testMsg = "bound success";
        },
        //Fail callback
        function(err){
            console.log("error");
            $scope.testMsg = "error";
        });
   });

   $cordovaNfcUtil.then(function(nfcUtil){
        console.log( nfcUtil.bytesToString("some bytes") )
        $scope.testMsg = "GOT ONE";
   });    

  // Update app code with new release from Ionic Deploy
  $scope.doUpdate = function() {
    deploy.update().then(function(res) {
      console.log('Ionic Deploy: Update Success! ', res);
    }, function(err) {
      console.log('Ionic Deploy: Update error! ', err);
    }, function(prog) {
      console.log('Ionic Deploy: Progress... ', prog);
    });
  };

  // Check Ionic Deploy for new code
  $scope.checkForUpdates = function() {
    console.log('Ionic Deploy: Checking for updates');
    deploy.check().then(function(hasUpdate) {
      console.log('Ionic Deploy: Update available: ' + hasUpdate);
      $scope.hasUpdate = hasUpdate;
    }, function(err) {
      console.error('Ionic Deploy: Unable to check for updates', err);
    });
  }

});
