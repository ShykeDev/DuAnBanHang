
myApp.controller('HomeCtrl', function ($scope, $http) {
    $scope.ListSanPham = [];
    $scope.ListDanhMuc = [];
    $scope.SanPhamModel = {};

    $scope.formatter = function (money) {
        return money.toLocaleString('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
    }

    $scope.getSanPham = function () {
        $http.get("/SanPhams/ListSanPham").then(function (response) {
            $scope.ListSanPham = response.data;
            console.log($scope.ListSanPham);
            setTimeout(() => $scope.getDanhMucs(), 500);
        }).catch(function (error) {
            $scope.getSanPham();
        });
    }
    $scope.getSanPham();
    $scope.getDanhMucs = function () {
        $http.get("/DanhMucs/GetDanhMuc").then(function (response) {
            $scope.ListDanhMuc = response.data;
            console.log($scope.ListDanhMuc);
        }).catch(function (error) {
            setTimeout(() => $scope.getDanhMucs(), 500);
        });
    }
});