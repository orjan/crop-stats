var app = angular.module('cropsTypeApp', ["api"]);

var api = angular.module("api", ["ngResource"]);

api.factory("CropType", function ($resource) {
    return $resource(
        "/api/croptype/:Id",
        {Id: "@Id" },
        { "update": { method: "PUT" } }
   );
});

app.controller('list', function ($scope, CropType) {
    $scope.items = CropType.query();
    $scope.item = new CropType();
    
    $scope.save = function () {
        if (!this.item.Id) {
            $scope.items.push(this.item);
        }
        
        this.item.$save();
        this.item = new CropType();
    };
    
    $scope.editRow = function (id) {
        $scope.item = $scope.items[id];
    };
    
    $scope.removeCrop = function (id) {
        var remove = $scope.items[id];
        $scope.items.splice(remove, 1);
        remove.$remove();
        this.item = new CropType();
    };
});