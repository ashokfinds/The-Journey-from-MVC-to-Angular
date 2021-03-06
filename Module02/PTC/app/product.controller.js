﻿(function () {
    'use strict';

    angular.module('app')
        .controller('ProductController', ProductController);

    function ProductController($http) {
        var vm = this;
        var dataService = $http;
        var emptySearchInput = function () {
            return {
                selectedCategory: {
                    CategoryId: 0,
                    CategoryName: ''
                },
                productName: ''
            };
        }

        // props
        vm.products = [];
        vm.searchCategories = [];
        vm.searchInput = emptySearchInput();
        // events
        vm.search = search;
        vm.searchImmediate = searchImmediate;
        vm.resetSearch = clearSearchInput;

        clearSearchInput();
        searchCategoriesList();

        function productList() {
            dataService.get("/api/Product")
                .then(function (result) {
                    vm.products = result.data;       
                    var x = 0;
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

        function search() {
            var searchEntity = {
                CategoryId:
                    vm.searchInput.selectedCategory.CategoryId,
                ProductName:
                    vm.searchInput.productName
            };

            dataService.post("/api/Product/Search", searchEntity)
                .then(function (result) {
                    vm.products = result.data;
                    var x = 0;
                }, function (error) {
                    handleException(error);
                }
            );
        }
        function searchImmediate(item) {
            var matchesCategory = vm.searchInput.selectedCategory.CategoryId === 0
                ? true
                : vm.searchInput.selectedCategory.CategoryId === item.Category.CategoryId;
            var matchesProductName = vm.searchInput.productName.length === 0
                ? true
                : (item.ProductName.toLowerCase().indexOf(vm.searchInput.productName.toLowerCase())) >= 0;

            return matchesCategory && matchesProductName;
        }

        function clearSearchInput() {
            vm.searchInput = emptySearchInput();
            productList();
        }

        function handleException(error) {
            alert(error.data.ExceptionMessage);
        }
    }
})();