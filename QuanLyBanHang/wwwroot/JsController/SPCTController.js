myApp.controller('SPChiTietCtrl', function ($scope, $http) {
    $scope.SanPham = {}
    $scope.GioHangChiTiet = {
        userId: "",
        sanPhamId: "",
        thuocTinh: "",
        soLuong: 1
    }

    $http.get("/SanPhams/GetSanPham?id=" + $("#ThongTinSanPham").val()).then(function (response) {
        $scope.SanPham = response.data;
        console.log($scope.SanPham)
    }).catch(function (error) {
        swal("Oops!", "Đã xảy ra lỗi!", "error");
    });

    $scope.ThemGioHang = function () {
        $scope.GioHangChiTiet.thuocTinh = "";
        $scope.GioHangChiTiet.userId = getCookie("ID");
        $scope.GioHangChiTiet.sanPhamId = $("#ThongTinSanPham").val();
        var thuocTinhList = document.getElementsByClassName("ThuocTinh");
        for (var i = 0; i < thuocTinhList.length; i++) {
            if (i == 0) $scope.GioHangChiTiet.thuocTinh += thuocTinhList[i].value;
            else $scope.GioHangChiTiet.thuocTinh += "/" + thuocTinhList[i].value;
        }
        console.log($scope.GioHangChiTiet)
        $.ajax({
            url: '/Home/AddGioHang',
            type: 'POST',
            data: $scope.GioHangChiTiet,
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    return;
                } else {
                    if (response == true) {
                        toastr.success("Thêm thành công");
                    } else {
                        toastr.error("Thêm thất bại");
                    }
                }
            },
            error: function () {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
                return;
            }
        })
    }

    $scope.soLuongCong = function () {
        $scope.GioHangChiTiet.soLuong++;
    }

    $scope.soLuongTru = function () {
        if ($scope.GioHangChiTiet.soLuong > 1)
            $scope.GioHangChiTiet.soLuong--;
    }
});