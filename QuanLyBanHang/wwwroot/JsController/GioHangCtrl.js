myApp.controller('GioHangCtrl', function ($rootScope, $scope, $http) {
    $scope.ListSanPham = [];
    $scope.ListThanhToan = [];
    $scope.ListHoaDon = [];
    $scope.ListGioHang = [];
    $scope.SelectAll = false;
    $scope.ListCheckBox = [];
    $scope.chuthich = "";
    $scope.filter = -1;
    $scope.sortType = 'ngayMua'
    $scope.sortReverse = true;
    $scope.info = {};

    $scope.TrangThai = [
        { state: "Chờ xác nhận", value: 0 },
        { state: "Đang vận chuyển", value: 1 },
        { state: "Giao hàng thành công", value: 2 },
        { state: "Đã hủy", value: 3 },
    ]

    $scope.formatDate = function (date) {
        var date = new Date(date);
        var hours = String(date.getHours()).padStart(2, '0');
        var minutes = String(date.getMinutes()).padStart(2, '0');
        var day = String(date.getDate()).padStart(2, '0');
        var month = String(date.getMonth() + 1).padStart(2, '0');
        var year = date.getFullYear();

        var formattedDate = day + '/' + month + '/' + year + ' ' + hours + ':' + minutes;
        return formattedDate;
    }


    $scope.GetSanPhamInfo = function (id) {
        return $scope.ListSanPham.find(x => x.id == id);
    }

    $scope.GetSoLuongHoaDon = function (state) {
        try {
            return $scope.ListHoaDon.find(x => x.trangThaiDonHang == state) == null;
        } catch (error) {

        }
    }

    $scope.GetSanPhams = function () {
        $http.get("/Home/GetSanPhamGioHang?id=" + getCookie("ID")).then(function (response) {
            $scope.ListSanPham = response.data.sanPham;
            $scope.ListGioHang = response.data.gioHang;
            $scope.ListHoaDon = response.data.hoaDon;
            if ($scope.ListSanPham != null) {
                for (var i = 0; i < $scope.ListSanPham.length; i++) {
                    $scope.ListSanPham[i].checked = false;
                }
            }
            if ($scope.ListHoaDon != null) {
                for (var i = 0; i < $scope.ListHoaDon.length; i++) {
                    $scope.ListHoaDon[i].TongTien = $scope.getTongTien2($scope.ListHoaDon[i].hoaDonChiTiets);
                }
            }
            console.log(response.data);
        }).catch(function (error) {
            setTimeout(() => $scope.GetSanPhams(), 500);
            console.log(error);
        });
    }
    $scope.GetSanPhams();

    $scope.ChangeCheckBoxSelectAll = function () {
        $scope.SelectAll = !$scope.SelectAll;
        for (var i = 0; i < $scope.ListGioHang.length; i++) {
            $scope.ListGioHang[i].checked = $scope.SelectAll;
        }
    }

    $scope.HuyDon = function (id) {
        swal({
            title: "Bạn có chắc chắn muốn hủy?",
            text: "Hành động này sẽ không thể hoàn tác!",
            icon: "info",
            buttons: true,
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/HoaDons/UpdateHoaDon',
                    type: 'post',
                    data: {
                        id: id,
                        state: 3
                    },
                    success: function (response) {
                        if (response.success == true) {
                            $scope.GetSanPhams();
                            toastr.success("Hủy đơn hàng thành công");
                        }
                    }
                })
            }
        });
    }

    $scope.getTongTien = function () {
        try {
            $scope.sum = 0;
            for (var i = 0; i < $scope.ListGioHang.length; i++) {
                if ($scope.ListGioHang[i].checked == true) {
                    $scope.sum += ($scope.ListGioHang[i].soLuong * $scope.GetSanPhamInfo($scope.ListGioHang[i].idSanPham).giaGiamGia);
                }
            }
            return $scope.sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        } catch (error) {

        }

    };

    $scope.getTongTien2 = function (item) {
        $scope.sum = 0;
        for (var i = 0; i < item.length; i++) {
            $scope.sum += (item[i].soLuong * item[i].giaSanPham);
        }
        return $scope.sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    };

    $scope.ChangeCheckbox = function () {
        for (var i = 0; i < $scope.ListGioHang.length; i++) {
            if ($scope.ListGioHang[i].checked) {
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
                if (getCookie("ID") != "") {
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
                    toastr.error("Chưa đăng nhập", "Thông báo");
                }

            } else {
                toastr.error("Đã hủy xóa", "Thông báo");
            }
        });
    };

    $scope.ChiTiet = function (id) {
        $scope.info = $scope.ListHoaDon.find(x => x.id == id);
        $('#MainModal2').modal('show');
    }
    $scope.XacNhanThanhToan = function () {
        if ($rootScope.UserInfo != false)
            $scope.info = $rootScope.UserInfo;
        $scope.ListThanhToan = [];
        for (var i = 0; i < $scope.ListGioHang.length; i++) {
            if ($scope.ListGioHang[i].checked == true) {
                $scope.ListGioHang[i].sanPham = $scope.GetSanPhamInfo($scope.ListGioHang[i].idSanPham);
                $scope.ListThanhToan.push($scope.ListGioHang[i]);
            }
        }
        if ($scope.ListGioHang.length == 0) {
            toastr.error("Không có sản phẩm nào trong giỏ");
            return;
        }
        if ($scope.ListThanhToan.length == 0) {
            toastr.error("Chưa chọn sản phẩm thanh toán");
            return;
        }
        $('#MainModal').modal('show');
        console.log($scope.ListThanhToan)
    }

    $scope.ThemHoaDon = function () {
        var now = new Date();
        var year = now.getFullYear();
        var month = String(now.getMonth() + 1).padStart(2, '0'); // January is 0!
        var date = String(now.getDate()).padStart(2, '0');
        var hours = String(now.getHours()).padStart(2, '0');
        var minutes = String(now.getMinutes()).padStart(2, '0');
        var seconds = String(now.getSeconds()).padStart(2, '0');

        var formattedDate = year + '-' + month + '-' + date + ' ' + hours + ':' + minutes + ':' + seconds + '.0000000';


        //validate scope.info.name
        if ($scope.info.name == null || $scope.info.name == undefined || $scope.info.name.trim().length == 0) {
            swal("Lỗi!", "Họ tên  không hợp lệ", "error");
            return;
        }

        //validate scope.info.sdt
        if ($scope.info.sdt == null || $scope.info.sdt == undefined || $scope.info.sdt.trim().length == 0 || !/^(0?)[0-9]{9}$/.test($scope.info.sdt)) {
            swal("Lỗi!", "Số điện thoại không hợp lệ", "error");
            return;
        }


        //validate scope.info.email
        if ($scope.info.email == null || $scope.info.email == undefined || $scope.info.email.trim().length == 0 || !/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($scope.info.email)) {
            swal("Lỗi!", "Vui lòng nhập email hợp lệ", "error");
            return;
        }

        //validate scope.info.diaChi
        if ($scope.info.diaChi == null || $scope.info.diaChi == undefined || $scope.info.diaChi.trim().length == 0) {
            swal("Lỗi!", "Địa chỉ  không hợp lệ", "error");
            return;
        }

        var data = {
            hoaDon: {
                UserID: $scope.info.id,
                nameUser: $scope.info.name,
                NgayMua: formattedDate,
                SDT: $scope.info.sdt,
                Email: $scope.info.email,
                DiaChi: $scope.info.diaChi,
                ChuThich: $scope.chuthich
            },
            hoaDonChiTiets: $scope.ListThanhToan
        }
        console.log(data)
        $.ajax({
            //Post data
            url: '/Home/ThemHoaDon',
            type: "post",
            data: data,
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    return;
                } else {
                    console.log(response)
                    if (response.success) {
                        toastr.success("Xác nhận đơn hàng thành công");
                        $scope.GetSanPhams();
                    } else {
                        toastr.error(response.message);
                    }
                }
            },
            error: function () {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
                return;
            }
        });

        $('#MainModal').modal('hide');
    }

});