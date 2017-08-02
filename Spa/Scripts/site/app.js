var AppName = "myApp";

(function (appName) {

    var app = angular.module(appName, ["ui.router", "LocalStorageModule"]);

    app.config(RouteConfig);
    RouteConfig.$inject = ["$stateProvider", "$urlRouterProvider"];  
    function RouteConfig($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");
        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "/scripts/templates/HomePage.html",
                controller: "loginController",
                controllerAs: "as"
            })
            .state("about", {
                url: "/about",
                templateUrl: "/scripts/templates/AboutPage.html"
            })
            .state("todolist", {
                url: "/todolist",
                templateUrl: "/scripts/templates/ToDoList.html",
                controller: "todo",
                controllerAs: "todos"
            })
            .state("todoeditor", {
                url: "/todoeditor/:id",
                templateUrl: "/scripts/templates/ToDoEditor.html",
                controller: "todoEditor",
                controllerAs: "editor"
            })
            .state("todolistandedit", {
                url: "/todolistandedit/:id",
                views: {
                    "": { templateUrl: "/scripts/templates/ListAndEdit.html" },
                    "list@todolistandedit": {
                        controller: "todo2",
                        controllerAs: "todos",
                        templateUrl: "/scripts/templates/ToDoList.html"
                    },
                    "edit@todolistandedit": {
                        controller: "todoEditor2",
                        controllerAs: "todoEditor",
                        templateUrl: "/scripts/templates/ToDoEditor.html"
                    }
                }
            })
            .state("states", {
                url: "/states",
                templateUrl: "/scripts/templates/states.html",
                controller: "statesController",
                controllerAs: "sc"
            })
    }

    app.factory("authInterceptorService", AuthInterceptorService);
    AuthInterceptorService.$inject = ["$q", "$location", "localStorageService", "$window"];
    function AuthInterceptorService($q, $location, localStorageService, $window) {

        var ais = {
            request: _request,
            responseError: _responseError
        }

        function _request(config) {
            config.headers = config.headers || {};
            var authData = localStorageService.get("authorizationData");
            if (authData) {
                config.headers.Token = authData.token;
                config.headers.TokenExpire = authData.tokenExpire;
            }
            return config;
        }

        function _responseError(rejection) {
            if (rejection.status > 200)
                $window.location.href = "/";

            return $q.reject(rejection);
        }

        return ais;
    }

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push("authInterceptorService");
    })

})(AppName);