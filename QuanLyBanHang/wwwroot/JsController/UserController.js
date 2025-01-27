myApp.controller('UsersCtrl', function ($scope, $http) {
    $scope.RoleClass = [
        {
            bg: "bg-info-faded",
            color: "text-info",
            name: "Admin"
        },
        {
            bg: "bg-danger-faded",
            color: "text-danger",
            name: "User"
        },
        {
            bg: "bg-warning-faded",
            color: "text-warning",
            name: "Personnel"
        }
    ]
    $scope.StateClass = [
        {
            class: "f-w-2 f-h-2 bg-success d-block rounded-circle me-1 fs-xs fw-bolder",
            name: "Active"
        },
        {
            class: "f-w-2 f-h-2 bg-danger d-block rounded-circle me-1 fs-xs fw-bolder",
            name: "Disable"
        }
    ]

    $scope.userModel = {};
    $scope.ListUsers = [];

    $scope.getUsers = function () {
        $http.get("/Users/GetAllUsers").then(function (response) {
            $scope.ListUsers = response.data;
            console.log($scope.ListUsers);
        }).catch(function (error) {
            setTimeout(() => $scope.getUsers(), 500);
        });
    }

    $scope.getUsers();

    $scope.OnAddUser = function () {
        $("#MainModalLabel").html("Thêm tài khoản");
        $("#ThemTaiKhoan").css("display", "block");
        $("#SuaTaiKhoan").css("display", "none");
        $("#Role").prop('disabled', false);
        $("#State").prop('disabled', false);
        $('#MainModal').modal('show');
        $scope.userModel = {
            id: "",
            name: "",
            userName: "",
            password: "",
            sdt: "",
            diaChi: "",
            email: "",
            ngaySinh: new Date("01-01-2000"),
            role: "1",
            state: "0"
        };
    }

    $scope.AddUser = function () {
        var datauser = {
            id: $scope.userModel.id,
            name: $scope.userModel.name,
            userName: $scope.userModel.userName,
            password: $scope.userModel.password,
            sdt: $scope.userModel.sdt,
            diaChi: $scope.userModel.diaChi,
            email: $scope.userModel.email,
            ngaySinh: formatDate($scope.userModel.ngaySinh),
            role: Number($scope.userModel.role),
            state: Number($scope.userModel.state)
        }
        $.ajax({
            url: '/Users/AddUser',
            type: 'post',
            data: datauser,
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    return;
                } else {
                    if (response == true) {
                        toastr.success("Thêm thành công");
                        $scope.getUsers();
                        $('#MainModal').modal('hide');
                    } else {
                        swal("Lỗi!", response, "error");
                    }
                }
            },
            error: function () {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
                return;
            }
        })
    }

    $scope.OnEditUser = function (id) {
        $("#MainModalLabel").html("Sửa tài khoản");
        $("#ThemTaiKhoan").css("display", "none");
        $("#SuaTaiKhoan").css("display", "block");
        $('#MainModal').modal('show');
        $http.get("/Users/GetUsersByID?id=" + id).then(function (response) {
            $scope.userModel = response.data;
            $scope.userModel.ngaySinh = new Date($scope.userModel.ngaySinh);
            $scope.userModel.role = "" + $scope.userModel.role;
            $scope.userModel.state = "" + $scope.userModel.state;
        }).catch(function (e) {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
            return;
        })
    }

    $scope.SaveUser = function () {
        var datauser = {
            id: $scope.userModel.id,
            name: $scope.userModel.name,
            userName: $scope.userModel.userName,
            password: $scope.userModel.password,
            sdt: $scope.userModel.sdt,
            diaChi: $scope.userModel.diaChi,
            email: $scope.userModel.email,
            ngaySinh: formatDate($scope.userModel.ngaySinh),
            role: Number($scope.userModel.role),
            state: Number($scope.userModel.state)
        }
        $.ajax({
            url: '/Users/SaveUser',
            type: 'post',
            data: datauser,
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    return;
                } else {
                    if (response == true) {
                        toastr.success("Sửa thành công");
                        $scope.getUsers();
                        $('#MainModal').modal('hide');
                    } else {
                        swal("Lỗi!", response, "warning");
                    }
                }
            },
            error: function () {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
                return;
            }
        })
    }


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
                    url: '/Users/DeleteUser',
                    type: "GET",
                    data: { id: id },
                    success: function (response) {
                        if (response == null || response == undefined || response.length == 0) {
                            return;
                        } else {
                            if (response) {
                                toastr.success("Xóa thành công", "Thông báo");
                                $scope.getUsers();
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
    }
});
