myApp.controller('GioHangCtrl', function ($rootScope, $scope, $http) {
    $scope.ListSanPham = [];
    $scope.ListThanhToan = [];
    $scope.SelectAll = false;
    $scope.ListCheckBox = [];
    $scope.chuthich = "";

    $scope.GetSanPhams = function () {
        $http.get("/Home/GetSanPhamGioHang?id=" + getCookie("ID")).then(function (response) {
            $scope.ListSanPham = response.data;
            for (var i = 0; i < $scope.ListSanPham.length; i++) {
                $scope.ListSanPham[i].checked = false;
            }
            console.log($scope.ListSanPham);
        }).catch(function (error) {
            setTimeout(() => $scope.GetSanPhams(), 500);
        });
    }
    $scope.GetSanPhams();

    $scope.ChangeCheckBoxSelectAll = function () {
        $scope.SelectAll = !$scope.SelectAll;
        for (var i = 0; i < $scope.ListSanPham.length; i++) {
            $scope.ListSanPham[i].checked = $scope.SelectAll;
        }
    }

    $scope.getTongTien = function () {
        $scope.sum = 0;
        for (var i = 0; i < $scope.ListSanPham.length; i++) {
            if ($scope.ListSanPham[i].checked == true) {
                $scope.sum += ($scope.ListSanPham[i].soLuong * $scope.ListSanPham[i].sanPham.giaGiamGia);
            }
        }
        return $scope.sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    };


    $scope.ChangeCheckbox = function () {
        for (var i = 0; i < $scope.ListSanPham.length; i++) {
            if ($scope.ListSanPham[i].checked) {
                $scope.SelectAll = false;
                return;
            }
        }
    }

    $scope.deleteItem = function (id) {
        swal({
            title: "Bạn có chắc chắn muốn xóa?",
            text: "Hành động này sẽ không thể hoàn tác!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Home/RemoveSanPhamGioHang',
                    type: "GET",
                    data: { id: id },
                    success: function (response) {
                        if (response == null || response == undefined || response.length == 0) {
                            return;
                        } else {
                            if (response) {
                                toastr.success("Xóa thành công", "Thông báo");
                                $scope.GetSanPhams();
                            } else {
                                swal("Oops!", "Xóa thất bại!", "error");
                            }
                        }
                    },
                    error: function () {
                        swal("Oops!", "Đã xảy ra lỗi!", "error");
                        return;
                    }
                })
            } else {
                toastr.error("Đã hủy xóa", "Thông báo");
            }
        });
    };


    $scope.XacNhanThanhToan = function () {
        $scope.ListThanhToan = [];
        for (var i = 0; i < $scope.ListSanPham.length; i++) {
            if ($scope.ListSanPham[i].checked == true) {
                $scope.ListThanhToan.push($scope.ListSanPham[i]);
            }
        }
        if ($scope.ListThanhToan.length == 0) {
            toastr.error("Chưa chọn sản phẩm thanh toán");
            return;
        }
        $('#MainModal').modal('show');
    }
});