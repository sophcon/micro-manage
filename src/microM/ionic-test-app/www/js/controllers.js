angular.module('starter.controllers', ["ngCordova.plugins.nfc"])
  .controller('DashCtrl', ['$scope', '$cordovaNfc', '$cordovaNfcUtil', function ($scope, $cordovaNfc, $cordovaNfcUtil) {

      //var deploy = new Ionic.Deploy();
      //Because of the problem about the async-ness of the nfc plugin, we need to wait
      //for it to be ready.
      $cordovaNfc.then(function (nfcInstance) {

          //Use the plugins interface as you go, in a more "angular" way
          nfcInstance.addNdefListener(function (event) {
              //Callback when ndef got triggered
              alert('something');
          })
          .then(
            //Success callback
            function (event) {
                alert("bound success");
            },
            //Fail callback
            function (err) {
                alert("error");
            });
      });

      $cordovaNfcUtil.then(function (nfcUtil) {
          alert(nfcUtil.bytesToString("some bytes"))
      });
      // Update app code with new release from Ionic Deploy
      $scope.doUpdate = function () {
          $scope.testMsg = "doUpdate clicked";
          //deploy.update().then(function (res) {
          //    $scope.testMsg = 'Ionic Deploy: Update Success! ';
          //}, function (err) {
          //    $scope.testMsg = 'Ionic Deploy: Update error! ';
          //}, function (prog) {
          //    $scope.testMsg = 'Ionic Deploy: Progress... ';
          //});
      };

      // Check Ionic Deploy for new code
      $scope.checkForUpdates = function () {
          $scope.testMsg = "checkForUpdates clicked";
          //console.log('Ionic Deploy: Checking for updates');
          //deploy.check().then(function (hasUpdate) {
          //    console.log('Ionic Deploy: Update available: ' + hasUpdate);
          //    $scope.hasUpdate = hasUpdate;
          //}, function (err) {
          //    console.error('Ionic Deploy: Unable to check for updates', err);
          //    $scope.hasUpdate = 'Error';
          //});
      }
  }]);
