(function () {
    'use strict';

    angular.module(AppName).controller("todo", ToDoController);
    ToDoController.$inject = ["$scope", "$state", "todoService"];

    function ToDoController($scope, $state, todoService) {

        var vm = this;
        vm.items = [];
        vm.selectedItem = {};
        vm.$oninit = _init();
        vm.edit = _edit;

        function _init() {
            todoService.GetToDoItems().then(function (data) {
                vm.items = data;
            }).catch(function (err) {
                console.log(err);
            })
        }

        function _edit(item) {
            $state.go("todoeditor", { id: item.id });
        }
    }
})();

(function () {

    'use strict';

    angular.module(AppName).controller("todoEditor", ToDoEditorController);
    ToDoEditorController.$inject = ["$scope", "$state", "todoService"];

    function ToDoEditorController($scope, $state, todoService) {

        var vm = this;
        vm.selectedItem = {};
        vm.save = _save;
        vm.$oninit = _loadForm();

        function _loadForm() {

            var id = $state.params.id;

            if (id) {
                vm.selectedItem = todoService.GetById(id);
            }
        }

        function _save() {

            todoService.SaveItem(vm.selectedItem);

            vm.selectedItem = {};

            $state.go("todolist");
        }
    }
})();

(function () {
    'use strict';

    angular.module(AppName).controller("todo2", ToDoController2);
    ToDoController2.$inject = ["$scope", "$state", "todoService"];

    function ToDoController2($scope, $state, todoService) {

        var vm = this;
        vm.items = [];
        vm.selectedItem = {};
        vm.$oninit = _init();
        vm.edit = _edit;

        function _init() {
            todoService.GetToDoItems().then(function (data) {
                vm.items = data;
            }).catch(function (err) {
                console.log(err);
            })
        }

        function _edit(item) {
            $state.go("todolistandedit", { id: item.id });
        }
    }
})();

(function () {

    'use strict';

    angular.module(AppName).controller("todoEditor2", ToDoEditorController2);
    ToDoEditorController2.$inject = ["$scope", "$state", "todoService"];

    function ToDoEditorController2($scope, $state, todoService) {

        var vm = this;
        vm.selectedItem = {};
        vm.save = _save;
        vm.$oninit = _loadForm();

        function _loadForm() {

            var id = $state.params.id;

            if (id) {
                vm.selectedItem = todoService.GetById(id);
            }
        }

        function _save() {

            todoService.SaveItem(vm.selectedItem);

            vm.selectedItem = {};

            $state.go("todolistandedit");
        }
    }
})();

(function () {
    'use strict';

    angular.module(AppName).controller("statesController", StatesController);
    StatesController.$inject = ["$scope", "$state", "statesService", "authService"];

    function StatesController($scope, $state, statesService, authService) {

        var vm = this;
        vm.items = [];
        vm.selectedItem = {};
        vm.$oninit = _init();
        vm.edit = _edit;
        vm.logOut = _logOut;

        function _init() {
            statesService.GetAllStates().then(function (data) {
                console.log('right here', data);
                vm.items = data;
            }).catch(function (err) {
                console.log(err);
            })
        }

        function _edit(item) {
            $state.reload("states", { id: item.id });
        }


        function _logOut() {
            console.log('loggin out');
            authService.logOut();
            $state.go("home");
        }
    }
})();

(function () {
    'use strict';

    angular.module(AppName).controller("loginController", LoginController);
    LoginController.$inject = ['$scope', '$location', '$window', 'authService', '$state'];

    function LoginController($scope, $location, $window, authService, $state) {

        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$window = $window;

        vm.loginData = {};
        vm.register = _register;
        vm.registerUser = {};
        vm.login = _login;


        function _login(data) {
            console.log(data);
            authService.login(data).then(function (response) {
                console.log(response);
                $state.go("states");
            }).catch(function (response) {
                console.log(response);
            })
        }

        function _register(data) {
            authService.post(data);
            $route.reload();
        }
    }

})();