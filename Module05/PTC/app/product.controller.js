﻿(function () {
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
        var emptyProduct = function () {
            return {
                ProductId: 0,
                Category: {
                    CategoryId: 1,
                    CategoryName: ''
                },
                ProductName: '',
                IntroductionDate: new Date().toLocaleDateString(),
                Price: 0,
                Url: 'http://'
            };
        };
        

        // props
        vm.uiState = initialUiState();
        vm.product = {};        
        vm.products = [];
        vm.categories = [];
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
            categoriesList();
            vm.product = emptyProduct();
            setUiState(pageMode.ADD);
        }
        function editClick(id) {
            categoriesList();
            productGet(id);
            setUiState(pageMode.EDIT);
        }
        function deleteClick(id) {
            if (confirm("Delete this Product?")) {
                productDelete(id);
            }
        }
        function saveClick(productForm) {
            if (productForm.$valid) {
                if (productValidate()) {
                    productForm.$setPristine();
                    if (vm.uiState.mode === pageMode.ADD) {
                        productAdd();
                    }
                    if (vm.uiState.mode === pageMode.EDIT) {
                        productUpdate();
                    }
                } else {
                    productForm.$valid = false;
                }
            } else {
                vm.uiState.isValid = false;
            }

        }
        function cancelClick(productForm) {
            productForm.$setPristine();
            productForm.$valid = true;
            vm.uiState.isValid = true;
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
        function productGet(id) {
            dataService.get("/api/Product/" + id)
                .then(function (result) {
                    vm.product = result.data;
                    if (vm.product.IntroductionDate != null) {
                        vm.product.IntroductionDate = new Date(vm.product.IntroductionDate)
                            .toLocaleDateString();
                    }
                }, function (error) {
                    handleException(error);
                }
            );
        }
        function productAdd() {
            dataService.post("/api/Product/", vm.product)
                .then(function (result) {
                    vm.product = result.data;
                    vm.products.push(vm.product);
                    setUiState(pageMode.LIST);
                }, function (error) {
                    handleException(error);
                }
            );
        }
        function productDelete(id) {
            dataService.delete("/api/Product/" + id)
                .then(function (result) {
                    var index = vm.products.map(function (p) {
                        return p.ProductId;
                    }).indexOf(id);
                    vm.products.splice(index, 1); // remove from array                    
                    setUiState(pageMode.LIST);
                }, function (error) {
                    handleException(error);
                }
            );
        }
        function productUpdate() {
            dataService.put("/api/Product/" + vm.product.ProductId, vm.product)
                .then(function (result) {
                    vm.product = result.data;

                    var index = vm.products.map(function (p) {
                        return p.ProductId;
                    }).indexOf(vm.product.ProductId);
                    vm.products[index] = vm.product;

                    setUiState(pageMode.LIST);
                }, function (error) {
                    handleException(error);
                }
            );
        }
        function productValidate() {           
            // IE11 workaround
            vm.product.IntroductionDate =
                vm.product.IntroductionDate.replace(/\u200E/g, '');


            vm.uiState.messages = [];

            // Validation
            if (vm.product.IntroductionDate !== null) {
                if (isNaN(Date.parse(vm.product.IntroductionDate))) {
                    addValidationMessage('IntroductionDate', 'Invalid Introduction Date');
                }
            }
            if (vm.product.Url.toLowerCase().indexOf('microsoft') >= 0) {
                addValidationMessage('Url', 'URL cannot contain the words "microsoft".');
            }

            vm.uiState.isValid = vm.uiState.messages.length === 0;

            return vm.uiState.isValid;
        }
        function addValidationMessage(property, message) {
            vm.uiState.messages.push({
                property: property,
                message: message
            });
        }
        function categoriesList() {
            dataService.get("/api/Category/")
                .then(function (result) {
                    vm.categories = result.data;
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
            vm.uiState.isSearchAreaVisible =
                state === pageMode.LIST;
        }

        function handleException(error) {
            vm.uiState.isValid = false;
            var message = {
                property: 'Error',
                message: ''
            };

            vm.uiState.messages = [];

            switch (error.status) {
                case 400: // bad request
                    var errors = error.data.ModelState;

                    for (var key in errors) {
                        for (var i = 0; i < errors[key].length; i++) {
                            addValidationMessage(key, errors[key][i]);
                        }
                    }

                    break;
                case 404: // not found
                    message.message = 'The product you were request could not be found.';
                    vm.uiState.messages.push(message);
                    break;
                case 500: // internal server error
                    message.message = 'Something went wrong on the server...: "' + error.data.ExceptionMessage + '".';
                    vm.uiState.messages.push(message);
                    break;
                default: // default behaviour when error
                    message.message = 'Status: "' + error.status + '" - Error Message: "' + error.statusText + '".'
                    vm.uiState.messages.push(message);
                    break;
            };
        }
    }
})();