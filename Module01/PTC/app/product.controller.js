(function () {
    'use strict';

    angular.module('app')
        .controller('ProductController', ProductController);

    function ProductController() {
        var vm = this;

        vm.product = {
            productId: 1,
            productName: 'Video Training'
        };
    }
})();