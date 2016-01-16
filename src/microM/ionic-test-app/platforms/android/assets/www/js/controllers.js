angular.module('starter.controllers', [])
  .controller('DashCtrl', ['$scope', function ($scope) {

      var deploy = new Ionic.Deploy();
      
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
