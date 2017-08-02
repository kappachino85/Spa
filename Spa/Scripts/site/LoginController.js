(function () {
    'use strict';

    angular.module(AppName).controller("loginController", LoginController);
    LoginController.$inject = ['$scope', '$location', '$window', 'authService'];

    function LoginController($scope, $location, $window, authService) {

        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$window = $window;

        vm.loginData = {};
        vm.register = _register;
        vm.registerUser = {};
        vm.login = _login;

        function _login(data) {
            console.log("fuck");
            //authService.login(data).then(function (response) {
            //    console.log(response);
            //}).catch(function (response) {
            //    console.log(response);
            //})
        }

        function _register(data) {
            console.log("yes");
           // vm.authService.post(data);
            //$route.reload();
        }
    }

})();