myApp.controller('HoaDonCtrl', function ($scope, $http, $window) {
    $scope.ListHoaDon = [];
    $scope.ListUser = [];
    $scope.ListSanPham = [];
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

    $scope.GetListHoaDon = function () {
        $http.get("/HoaDons/GetAllHoaDon").then(function (response) {
            if (response.data.success == true) {
                $scope.ListHoaDon = response.data.hoaDon;
                $scope.ListSanPham = response.data.sanPham;
                for (var i = 0; i < $scope.ListHoaDon.length; i++) {
                    $scope.ListHoaDon[i].TongTien = $scope.getTongTien($scope.ListHoaDon[i].hoaDonChiTiets);
                }
                $scope.ListUser = response.data.user;
                console.log(response.data);
            }
        }).catch(function (error) {
            console.log(error);
            setTimeout(() => $scope.GetListHoaDon(), 500);
        })
    }

    $scope.GetSanPham = function(id) {
        return $scope.ListSanPham.find(x => x.id == id);
    }

    $scope.ChiTiet = function (id) {
        $scope.info = $scope.ListHoaDon.find(x => x.id == id);
        $('#MainModal').modal('show');
    }

    $scope.getTongTien = function (item) {
        try {
            $scope.sum = 0;
            for (var i = 0; i < item.length; i++) {
                $scope.sum += (item[i].soLuong * item[i].giaSanPham);
            }
            return $scope.sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        } catch (error) {

        }
    };

    $scope.Update = function (id, state) {
        swal({
            title: "Cập nhật hóa đơn?",
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
                        state: state
                    },
                    success: function (response) {
                        if (response.success == true) {
                            $scope.GetListHoaDon();
                            toastr.success("Cập nhật hóa đơn thành công");
                        }
                    }
                })
            }
        });
    }


    $scope.ChangeState = function (state) {
        $scope.filter = state;
    }

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



    $scope.DeleteHoaDon = function (id) {
        if (confirm("Bạn có chắc chắn muốn xóa không?")) {
            $http.get("/HoaDon/DeleteHoaDon?id=" + id).then(function (response) {
                $scope.GetListHoaDon();
            })
        }
    }

    $scope.GetListHoaDon();
});
