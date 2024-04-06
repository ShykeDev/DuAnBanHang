
myApp.controller('SanPhamCtrl', function ($scope, $http) {
    $scope.ListSanPham = [];
    $scope.ListDanhMuc = [];
    $scope.SanPhamModel = {};

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

    $scope.OnEditSP = function (id) {
        $("#MainModalLabel").html("Sửa sản phẩm");
        $("#ThemThuocTinh").css("display", "none");
        $("#SuaThuocTinh").css("display", "block");
        $('#MainModal').modal('show');
        $http.get("/SanPhams/GetSanPham?id=" + id).then(function (response) {
            $scope.SanPhamModel = response.data;
            console.log($scope.SanPhamModel)

        }).catch(function (error) {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
        });
    }


});