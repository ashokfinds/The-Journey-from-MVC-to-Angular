(function () {
    'use strict';

    angular.module('app')
        .controller('ProductController', ProductController);

    function ProductController($http) {
        var vm = this;
        var dataService = $http;

        vm.products = [];

        productList();

        function productList() {
            dataService.get("/api/Product")
                .then(function (result) {
                    vm.products = result.data;                    
                }, function (error) {
                    handleException(error);
                }
            );
        }

        function handleException(error) {
            alert(error.data.ExceptionMessage);
        }
    }
})();