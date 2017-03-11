(function () {
    'use strict';

    angular.module('app')
        .controller('ProductController', ProductController);

    function ProductController($http) {        
        var vm = this;
        var dataService = $http;
        var pageMode = {
            LIST: 'List',
            EDIT: 'Edit',
            ADD: 'Add'
        };
        var initialUiState = function () {
            return {
                mode: pageMode.LIST,
                isDetailAreaVisible: false,
                isListAreaVisible: true,
                isSearchAreaVisible: true,
                isValid: true,
                messages: []
            };
        };
        var emptySearchInput = function () {
            return {
                selectedCategory: {
                    CategoryId: 0,
                    CategoryName: ''
                },
                productName: ''
            };
        };
        

        // props
        vm.uiState = initialUiState();
        vm.products = [];
        vm.searchCategories = [];
        vm.searchInput = emptySearchInput();
        // events
        vm.search = search;
        vm.searchImmediate = searchImmediate;
        vm.resetSearch = clearSearchInput;
        vm.addClick = addClick;
        vm.editClick = editClick;
        vm.deleteClick = deleteClick;
        vm.saveClick = saveClick;
        vm.cancelClick = cancelClick;

        clearSearchInput();
        searchCategoriesList();

        function addClick() {
            setUiState(pageMode.ADD);
        }
        function editClick(id) {
            setUiState(pageMode.EDIT);
        }
        function deleteClick(id) {
            if (confirm("Delete this Product?")) {
            }
        }
        function saveClick() {
            setUiState(pageMode.LIST);
        }
        function cancelClick() {
            setUiState(pageMode.LIST);
        }

        function productList() {
            dataService.get("/api/Product")
                .then(function (result) {                    
                    vm.products = result.data;
                    setUiState(pageMode.LIST);
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
                    setUiState(pageMode.LIST);
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

        function setUiState(state) {
            vm.uiState.mode = state;

            vm.uiState.isDetailAreaVisible =
                state === pageMode.ADD || state === pageMode.EDIT;
            vm.uiState.isListAreaVisible =
                state === pageMode.LIST;
            vm.isSearchAreaVisible =
                state === pageMode.LIST;
        }

        function handleException(error) {
            vm.uiState.isValid = false;
            alert(error.data.ExceptionMessage);
        }
    }
})();