// declare a module
var cropsApp = angular.module('cropsApp', []);

// configure the module.
// in this example we will create a greeting filter
cropsApp.filter('greet', function () {
    return function (name) {
        return 'Hello, ' + name + '!';
    };
});

cropsApp.factory('storage', function() {
    return {
        save: function(list) {
            var stringify = JSON.stringify(list);
            localStorage.setItem("crops", stringify);
        },
        load: function () {
            var item = localStorage.getItem("crops");
            return JSON.parse(item);
        }
    };
});

cropsApp.controller('cropList', function ($scope, storage, $http) {
    var initCrop = function() {
        return {
            area: "",
            type: "",
            output: "",
            isCalculated: false,
        };
    };

    $scope.cropList = storage.load() || [];
    $scope.crop = initCrop();
    $http.get("/api/croptype").success(function (data, status) {
        $scope.items = data;
    });

    $scope.addTodo = function () {
        var copy = angular.copy($scope.crop);
        $scope.cropList.push(copy);
        storage.save($scope.cropList);
        
        $scope.crop = initCrop();
    };

    $scope.editRow = function (id) {
        var crop = $scope.cropList[id];
        console.log(crop);
        $scope.crop = crop;
    };
    
    $scope.removeCrop = function (id) {
        var crop = $scope.cropList[id];
        $scope.cropList.splice(crop, 1);
        storage.save($scope.cropList);
    };

});