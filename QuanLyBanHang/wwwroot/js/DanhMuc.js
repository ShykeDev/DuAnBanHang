LoadDanhMuc()
function LoadDanhMuc() {
    var tbody = document.getElementById("myTable");
    tbody.innerHTML = "";

    $.ajax({
        url: '/DanhMucs/GetDanhMuc',
        type: "GET",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
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

                    var soluong = document.createElement("td");
                    soluong.textContent = item.danhMucChiTiets.length;
                    row.appendChild(soluong);

                    var actionsCell = document.createElement("td");
                    var suaButton = document.createElement("button");
                    var iconSua = document.createElement("i");
                    iconSua.classList.add("fa", "fa-pen", "ms-2")
                    suaButton.textContent = "Sửa";
                    suaButton.classList.add("btn", "btn-primary", "text-white", "btn-sm", "ps-3", "pe-3", "me-2");
                    suaButton.setAttribute("data-bs-toggle", "modal");
                    suaButton.setAttribute("data-bs-target", "#MainModal");
                    suaButton.dataset.target = "#MainModal";
                    suaButton.onclick = function () {
                        //SuaSanPham(item.id);
                    };
                    suaButton.appendChild(iconSua)
                    actionsCell.appendChild(suaButton);

                    var xoaButton = document.createElement("button");
                    var iconXoa = document.createElement("i");
                    iconXoa.classList.add("fa", "fa-trash", "ms-2")
                    xoaButton.textContent = "Xóa";
                    xoaButton.classList.add("btn", "btn-danger", "text-white", "btn-sm", "ps-3", "pe-3");
                    xoaButton.onclick = function () {
                        //XoaSanPham(item.id);
                    };
                    xoaButton.appendChild(iconXoa);
                    actionsCell.appendChild(xoaButton);

                    row.appendChild(actionsCell);

                    tbody.appendChild(row);
                }
            }
        },
        error: function () {
            sleep(1500);
            LoadDanhMuc();
            return;
        }
    })
}

function ThemDanhMuc() {
    $("#Name").val(null);
    $("#ThemDanhMuc").css("display", "block");
    $("#SuaDanhMuc").css("display", "none");
}

function OnThemDanhMuc() {
    $.ajax({
        url: '/DanhMucs/Create',
        data: {
            name: $("#Name").val()
        },
        type: "POST",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            } else {
                if (response == true) {
                    HideModal();
                    LoadDanhMuc();
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

function HideModal() {
    $('#MainModal').modal('hide');
}
