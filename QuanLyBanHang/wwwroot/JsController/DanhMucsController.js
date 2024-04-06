
myApp.controller('DanhMucsCtrl', function ($scope, $http) {
    $scope.ListDanhMuc = [];
    $scope.DanhMucModel = {};

    $scope.getDanhMucs = function () {
        $http.get("/DanhMucs/GetDanhMuc").then(function (response) {
            $scope.ListDanhMuc = response.data;
            console.log($scope.ListDanhMuc);
        }).catch(function (error) {
            setTimeout(() => $scope.getDanhMucs(), 500);
        });
    }
    $scope.getDanhMucs();

    $scope.ThemDanhMuc = function () {
        $("#MainModalLabel").html("Thêm danh mục");
        $("#ThemDanhMuc").css("display", "block");
        $("#SuaDanhMuc").css("display", "none");
        $scope.DanhMucModel = {
            id: "",
            name: "",
            danhMucChiTiets: []
        }
    }

    $scope.OnThemDanhMuc = function () {
        var data = {
            id: null,
            name: $scope.DanhMucModel.name,
            danhMucChiTiets: []
        }
        $.ajax({
            url: '/DanhMucs/Create',
            data: data,
            type: "POST",
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    swal("Oops!", "Đã xảy ra lỗi!", "error");
                } else {
                    if (response == true) {
                        $('#MainModal').modal('hide');
                        $scope.getDanhMucs();
                        toastr.success("Thêm thành công");
                    } else {
                        swal("Oops!", response, "error");
                    }
                }
            },
            error: function () {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            }
        })
    }

    $scope.EditDanhMuc = function (id) {
        $("#MainModalLabel").html("Sửa danh mục");
        $("#ThemDanhMuc").css("display", "none");
        $("#SuaDanhMuc").css("display", "block");
        $http.get("/DanhMucs/GetDanhMucByID?id=" + id).then(function (response) {
            $scope.DanhMucModel = response.data;
        }).catch(function (error) {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
        });
    }

    $scope.OnSave = function () {
        $.ajax({
            url: '/DanhMucs/SaveDanhMuc',
            data: $scope.DanhMucModel,
            type: "POST",
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    swal("Oops!", "Đã xảy ra lỗi!", "error");
                } else {
                    if (response == true) {
                        $('#MainModal').modal('hide');
                        $scope.getDanhMucs();
                        toastr.success("Sửa thành công");
                    } else {
                        swal("Oops!", response, "error");
                    }
                }
            },
            error: function () {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            }
        })
    };

    $scope.Delete = function (id) {
        swal({
            title: "Bạn có chắc chắn muốn xóa?",
            text: "Hành động này sẽ không thể hoàn tác!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/DanhMucs/DeleteDanhMuc',
                    type: "GET",
                    data: { id: id },
                    success: function (response) {
                        if (response == null || response == undefined || response.length == 0) {
                            swal("Oops!", response, "error");
                        } else {
                            if (response == true) {
                                toastr.success("Xóa thành công", "Thông báo");
                                $scope.getDanhMucs();
                            } else {
                                swal("Oops!", response, "error");
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
    }
});