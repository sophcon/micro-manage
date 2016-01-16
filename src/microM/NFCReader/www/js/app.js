// (c) 2014 Don Coleman
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

angular.module('ionicNFC', ['ionic', 'nfcFilters'])
    .config(['$httpProvider', function($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }])
    .controller('MainController', function ($scope, $http, nfcService) {

        $scope.modeList = [ { id: 0, name: 'Adjust In' }, { id: 3, name: 'Adjust Out' }, { id: 2, name: 'Catalog' } ];
        $scope.selectedMode = $scope.modeList[0];
        $scope.tag = nfcService.tag;

        $scope.modeClicked = function(mode) {
          $scope.selectedMode = mode;
        }

        $scope.clear = function() {
            nfcService.clearTag();
        };

    })

    .factory('nfcService', function ($rootScope, $ionicPlatform) {

        var tag = {};

        $ionicPlatform.ready(function() {
            nfc.addNdefListener(function (nfcEvent) {
                //console.log(JSON.stringify(nfcEvent.tag, null, 4));
                $rootScope.$apply(function(){
                    angular.copy(nfcEvent.tag, tag);
                    var url = "";
                    var tagId = nfc.bytesToHexString(tag.id);
                    $.support.cors = true;
                    $.mobile.allowCrossDomainPages = true;

                    if($scope.selectedMode.id == 0) {
                        url = "http://micromanage.azurewebsites.net/pi/AddInventory";
                    } else if($scope.selectedMode.id == 1) {
                        url = "http://micromanage.azurewebsites.net/pi/RemoveInventory";
                    } else {
                        //url = "http://micromanage.azurewebsites.net/pi/AddInventory";
                    }

                    if(url != "") {
                        $.ajax({
                            url: url,
                            type: "POST",
                            data: { binId: 1, serialId: tagId },
                            crossDomain: true,
                            dataType: 'application/json',
                            success: function(data) {
                              
                            },
                            error: function(e) {
                              $scope.msg = JSON.stringify(e);
                            }
                        });
                    }
                    // if necessary $state.go('some-route')
                });
            }, function () {
                console.log("Listening for NDEF Tags.");
            }, function (reason) {
                alert("Error adding NFC Listener " + reason);
            });

        });

        return {
            tag: tag,

            clearTag: function () {
                angular.copy({}, this.tag);
            }
        };
    });