(function () {
    'use strict';

    angular.module(AppName).factory('todoService', ToDoService);
    ToDoService.$inject = ['$q'];

    function ToDoService($q) {

        var todoItems = [{
            id: 1,
            title: "Get Milk",
            desc: "On the way home, get milk"
        },
        {
            id: 2,
            title: "Get Gas",
            desc: "On the way home, stop by a gas station"
        },
        {
            id: 3,
            title: "Pay Rent",
            desc: "On the way home, drop off a check at the office"
        },
        {
            id: 4,
            title: "Cook Dinner",
            desc: "Meal prep for the week"
        }];

        var srv = {
            GetToDoItems: _getToDoItems,
            GetById: _getById,
            SaveItem: _save
        }

        return srv;

        function _getToDoItems() {
 
            var defer = $q.defer();

            if (todoItems) {
                defer.resolve(todoItems);
            } 
            else {
                defer.reject("No Items Found")
            }
            return defer.promise;

        }

        function _getById(id) {
            return todoItems.filter(function (obj) {
                return (obj.id == id);
            })[0];
        }

        function _save(itm) {
            var ndx = todoItems.indexOf(itm);
            if (ndx !== -1) {
                todoItems[ndx] = itm;
            } else {
                var maxid = 0;
                todoItems.map(function (obj) {
                    if (obj.id > maxid)
                        maxid = obj.id;
                });
                itm.id = maxid + 1;
                todoItems.push(itm);
            }
        }
    }
})();

(function () {
    'use strict';

    angular.module(AppName).factory('statesService', ToDoService);
    ToDoService.$inject = ['$q', '$http'];

    function ToDoService($q, $http) {

        var todoItems = [];

        var srv = {
            GetAllStates: _getToDoItems,
            GetById: _getById,
            SaveItem: _save
        }

        return srv;

        function _getToDoItems() {

            return $http.get("/api/states")
                .then(getSuccess)
                .catch(getError)

            function getSuccess(response) {
                //console.log(response);
                return response.data;
            }

            function getError(response) {
                return $q.reject(response);
            }

        }

        function _getById(id) {
            return todoItems.filter(function (obj) {
                return (obj.id == id);
            })[0];
        }

        function _save(itm) {
            var ndx = todoItems.indexOf(itm);
            if (ndx !== -1) {
                todoItems[ndx] = itm;
            } else {
                var maxid = 0;
                todoItems.map(function (obj) {
                    if (obj.id > maxid)
                        maxid = obj.id;
                });
                itm.id = maxid + 1;
                todoItems.push(itm);
            }
        }
    }
})();