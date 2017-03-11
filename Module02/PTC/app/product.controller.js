(function () {
    'use strict';

    angular.module('app')
        .controller('ProductController', ProductController);

    function ProductController($http) {
        var vm = this;
        var dataService = $http;

        vm.products = [];
        vm.searchCategories = [];
        vm.searchInput = {
            selectedCategory: {
                CategoryId: 0,
                CategoryName: ''
            },
            productName: ''
        };

        productList();
        searchCategoriesList();

        function productList() {
            dataService.get("/api/Product")
                .then(function (result) {
                    vm.products = result.data;                    
                }, function (error) {
                    handleException(error);
                }
            );
        }
        function searchCategoriesList() {
            dataService.get("/api/Category/GetSearchCategories")
                .then(function (result) {
                    vm.searchCategories = result.data;
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