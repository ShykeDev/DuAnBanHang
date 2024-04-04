var RoleClass = [
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


var StateClass = [
    {
        class: "f-w-2 f-h-2 bg-success d-block rounded-circle me-1 fs-xs fw-bolder",
        name: "Active"
    },
    {
        class: "f-w-2 f-h-2 bg-danger d-block rounded-circle me-1 fs-xs fw-bolder",
        name: "Disable"
    }
]

OnLoadUser()
function OnLoadUser() {
    var tbody = document.getElementById("myTable");
    tbody.innerHTML = "";
    $.ajax({
        url: '/Users/GetAllUsers',
        type: "GET",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                sleep(1500);
                OnLoadUser();
                return;
            } else {
                var model = response;
                model.forEach(addTable);

                function addTable(item) {
                    var row = document.createElement("tr");

                    // Tạo các cột cho mỗi dòng
                    var nameCell = document.createElement("td");
                    nameCell.textContent = item.name;
                    row.appendChild(nameCell);

                    var userName = document.createElement("td");
                    userName.textContent = item.userName;
                    row.appendChild(userName);

                    var password = document.createElement("td");
                    password.textContent = item.password;
                    row.appendChild(password);

                    var Role = document.createElement("td");
                    var badge = document.createElement("small");
                    badge.classList.add("badge", "rounded-pill", RoleClass[item.role].bg, RoleClass[item.role].color);
                    badge.textContent = RoleClass[item.role].name;
                    Role.append(badge);
                    row.appendChild(Role);
                    var DivState = document.createElement("div");
                    DivState.classList += "d-flex align-items-center justify-content-center";
                    var State = document.createElement("td");
                    var span1 = document.createElement("span");
                    var span2 = document.createElement("span");
                    span1.classList += StateClass[item.state].class;
                    span2.classList += "small text-muted";
                    span2.textContent = StateClass[item.state].name;
                    DivState.append(span1);
                    DivState.append(span2);
                    State.append(DivState);
                    row.appendChild(State);

                    var actionsCell = document.createElement("td");
                    var suaButton = document.createElement("button");
                    var iconSua = document.createElement("i");
                    iconSua.classList.add("fa", "fa-pen", "ms-2")
                    suaButton.textContent = "Sửa";
                    suaButton.classList.add("btn", "btn-primary", "text-white", "btn-sm", "ps-3", "pe-3", "me-2");
                    suaButton.dataset.target = "#MainModal";
                    suaButton.onclick = function () {
                        SuaTaiKhoan(item.id);
                    };
                    suaButton.appendChild(iconSua)
                    actionsCell.appendChild(suaButton);

                    var xoaButton = document.createElement("button");
                    var iconXoa = document.createElement("i");
                    iconXoa.classList.add("fa", "fa-trash", "ms-2")
                    xoaButton.textContent = "Xóa";
                    xoaButton.classList.add("btn", "btn-danger", "text-white", "btn-sm", "ps-3", "pe-3");
                    xoaButton.onclick = function () {
                        XoaTaiKhoan(item.id);
                    };
                    xoaButton.appendChild(iconXoa);
                    if (item.role != 0)
                        actionsCell.appendChild(xoaButton);

                    row.appendChild(actionsCell);

                    tbody.appendChild(row);
                }
            }
        },
        error: function () {
            sleep(1500);
            OnLoadUser();
            return;
        }
    })
}

function OnThemTaiKhoan() {

    $.ajax({
        url: '/Users/AddUser',
        type: 'post',
        data:
        {
            id: $("#ID").val(),
            name: $("#Name").val(),
            userName: $("#UserName").val(),
            password: $("#Password").val(),
            sDT: $("#SDT").val(),
            diaChi: $("#DiaChi").val(),
            email: $("#Email").val(),
            ngaySinh: $("#NgaySinh").val(),
            role: $("#Role").val(),
            state: $("#State").val()
        },
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                return;
            } else {
                if (response == true) {
                    toastr.success("Sửa thành công");
                    OnLoadUser();
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

function ThemTaiKhoan() {
    $("#MainModalLabel").html("Thêm tài khoản");
    $("#ThemTaiKhoan").css("display", "block");
    $("#SuaTaiKhoan").css("display", "none");
    $("#Role").prop('disabled', false);
    $("#State").prop('disabled', false);
    $("#ID").val(null);
    $("#Name").val(null);
    $("#SDT").val(null);
    $("#UserName").val(null);
    $("#Password").val(null);
    $("#DiaChi").val(null);
    $("#Email").val(null);
    $("#NgaySinh").val("2000-01-01");
    $("#Role").val(1);
    $("#State").val(0);
    $('#MainModal').modal('show');
}

function OnSaveUser() {
    $.ajax({
        url: '/Users/SaveUser',
        type: 'post',
        data:
        {
            id: $("#ID").val(),
            name: $("#Name").val(),
            userName: $("#UserName").val(),
            password: $("#Password").val(),
            sDT: $("#SDT").val(),
            diaChi: $("#DiaChi").val(),
            email: $("#Email").val(),
            ngaySinh: $("#NgaySinh").val(),
            role: $("#Role").val(),
            state: $("#State").val(),
        },
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                return;
            } else {
                console.log(response);
                if (response == true) {
                    toastr.success("Sửa thành công");
                    OnLoadUser();
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

function SuaTaiKhoan(id) {
    $("#MainModalLabel").html("Sửa tài khoản");
    $("#ThemTaiKhoan").css("display", "none");
    $("#SuaTaiKhoan").css("display", "block");
    $.ajax({
        url: '/Users/GetUsersByID',
        type: "GET",
        data: { id: id },
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                return;
            } else {
                var model = response;
                $("#ID").val(model.id);
                $("#Name").val(model.name);
                $("#SDT").val(model.sdt);
                $("#UserName").val(model.userName);
                $("#Password").val(model.password);
                $("#DiaChi").val(model.diaChi);
                $("#Email").val(model.email);
                $("#NgaySinh").val(model.ngaySinh);
                $("#Role").val(model.role);
                $("#State").val(model.state);
                if (model.role == 0) {
                    $("#Role").prop('disabled', true);
                    $("#State").prop('disabled', true);
                } else {
                    $("#Role").prop('disabled', false);
                    $("#State").prop('disabled', false);
                }

                $('#MainModal').modal('show');
            }
        },
        error: function () {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
            return;
        }
    })
}



function XoaTaiKhoan(id) {

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
                            OnLoadUser();
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

