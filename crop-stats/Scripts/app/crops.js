// declare a module
var cropsApp = angular.module('cropsApp', []);

// configure the module.
// in this example we will create a greeting filter
cropsApp.filter('greet', function () {
    return function (name) {
        return 'Hello, ' + name + '!';
    };
});

cropsApp.controller('cropList', function ($scope) {
    $scope.cropList = ["havre"];
    $scope.apa = "Örjan";

    $scope.addTodo = function () {
        $scope.cropList.push($scope.cropType);
    };

});