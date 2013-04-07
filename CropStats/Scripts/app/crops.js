var cropsApp = angular.module('cropsApp', ["cropService"]);

var api = angular.module("cropService", ["ngResource"]);
api.factory("Crop", function ($resource) {
    return $resource(
        "/api/crops/:Id",
        {Id: "@Id" },
        { "update": { method: "PUT" } }
   );
});
api.factory("CropType", function ($resource) {
    return $resource(
        "/api/croptype/:Id",
        {Id: "@Id" },
        { "update": { method: "PUT" } }
   );
});

cropsApp.filter('viewestimated', function () {
    return function (estimated) {
        if (estimated) {
            return "Uppskattad skörd";

        } else {
            return "Avräknad skörd";
        }
    }
});

cropsApp.filter('viewcrop', function () {
    return function (parameters) {
        return this.items.filter(function(x) {
            return x.Id == parameters;
        })[0].Name;
    };
});


cropsApp.controller('cropList', function ($scope, Crop, CropType) {
    $scope.crop = new Crop();
    $scope.cropList = Crop.query();
    $scope.items = CropType.query();
    
    $scope.saveCrop = function () {
        console.log(this.crop);
        if (!this.crop.Id) {
            this.crop.$save();
            $scope.cropList.push(this.crop);
            this.crop = new Crop();
            
        } else {
            this.crop.$save();
            this.crop = new Crop();
        }
    };

    $scope.editRow = function (id) {
        var crop = $scope.cropList[id];
        console.log(crop);
        $scope.crop = crop;
    };

    $scope.removeCrop = function (id) {
        var crop = $scope.cropList[id];
        $scope.cropList.splice(crop, 1);
        crop.$remove();
        this.crop = new Crop();
    };

});