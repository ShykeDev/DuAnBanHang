
var sanPhamModel2 = {
    ID: null,
    Name: "",
    GiaGoc: 0,
    GiaGiamGia: 0,
    SoLuong: 0,
    TrangThai: 2,
    MoTa: ""
}


function UpdateImage() {
    const file = [0];
    const reader = new FileReader();
    reader.onloadend = () => {
        const base64String = reader.result
            .replace('data:', '')
            .replace(/^.+,/, '');
        console.log(base64String);
    };
    reader.readAsDataURL(file);
}


OnLoadSanPham()
function OnLoadSanPham() {
    var tbody = document.getElementById("myTable");
    tbody.innerHTML = "";

    $.ajax({
        url: '/SanPhams/ListSanPham',
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

                    var giaGocCell = document.createElement("td");
                    giaGocCell.textContent = item.giaGoc;
                    row.appendChild(giaGocCell);

                    var giaGiamGiaCell = document.createElement("td");
                    giaGiamGiaCell.textContent = item.giaGiamGia;
                    row.appendChild(giaGiamGiaCell);

                    var soLuongCell = document.createElement("td");
                    soLuongCell.textContent = item.soLuong;
                    row.appendChild(soLuongCell);

                    var trangThaiCell = document.createElement("td");
                    trangThaiCell.textContent = item.trangThai;
                    row.appendChild(trangThaiCell);

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
                        SuaSanPham(item.id);
                    };
                    suaButton.appendChild(iconSua)
                    actionsCell.appendChild(suaButton);

                    var xoaButton = document.createElement("button");
                    var iconXoa = document.createElement("i");
                    iconXoa.classList.add("fa", "fa-trash", "ms-2")
                    xoaButton.textContent = "Xóa";
                    xoaButton.classList.add("btn", "btn-danger", "text-white", "btn-sm", "ps-3", "pe-3");
                    xoaButton.onclick = function () {
                        XoaSanPham(item.id);
                    };
                    xoaButton.appendChild(iconXoa);
                    actionsCell.appendChild(xoaButton);

                    row.appendChild(actionsCell);

                    tbody.appendChild(row);
                }
            }
        },
        error: function () {
            location.reload();
            return;
        }
    })
}

function OnThemSanPham() {
    sanPhamModel2 = {
        ID: null,
        Name: "",
        GiaGoc: 0,
        GiaGiamGia: 0,
        SoLuong: 0,
        TrangThai: 2,
        MoTa: ""
    }
    var DanhSachThuocTinhChung = [];
    getListThuocTinhDaiDien().forEach(fix);
    function fix(item) {
        var ThuocTinhChung = {
            ID: null,
            TenThuocTinh: item.TenThuocTinh
        }
        var ThuocTinh = {
            thuocTinhChung: ThuocTinhChung,
            giaTriThuocTinhs: item.giaTriThuocTinhs
        }
        DanhSachThuocTinhChung.push(ThuocTinh);
    }
    sanPhamModel2.Name = $("#Name").val();
    sanPhamModel2.GiaGoc = $("#GiaGoc").val();
    sanPhamModel2.GiaGiamGia = $("#GiaGiamGia").val();
    sanPhamModel2.SoLuong = $("#SoLuong").val();
    sanPhamModel2.MoTa = $("#MoTa").val();
    $.ajax({
        url: '/SanPhams/CreateSanPham',
        data: {
            sanPham: sanPhamModel2,
            ThuocTinhChungs: DanhSachThuocTinhChung,
            ImgsPath: document.getElementById('ImgView').src
        },
        type: "POST",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            } else {
                toastr.success(response);
                HideModal();
                ThemSanPham();
                OnLoadSanPham();
            }
        },
        error: function () {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
        }
    })
}

function OnSuaSanPham() {
    sanPhamModel2 = {
        ID: null,
        Name: "",
        GiaGoc: 0,
        GiaGiamGia: 0,
        SoLuong: 0,
        TrangThai: 2,
        MoTa: ""
    }
    var DanhSachThuocTinhChung = [];
    getListThuocTinhDaiDien().forEach(fix);
    function fix(item) {
        var ThuocTinhChung = {
            ID: null,
            TenThuocTinh: item.TenThuocTinh
        }

        var ThuocTinh = {
            thuocTinhChung: ThuocTinhChung,
            giaTriThuocTinhs: item.giaTriThuocTinhs
        }
        DanhSachThuocTinhChung.push(ThuocTinh);
    }
    sanPhamModel2.ID = $("#ID").val();
    sanPhamModel2.Name = $("#Name").val();
    sanPhamModel2.GiaGoc = $("#GiaGoc").val();
    sanPhamModel2.GiaGiamGia = $("#GiaGiamGia").val();
    sanPhamModel2.SoLuong = $("#SoLuong").val();
    sanPhamModel2.MoTa = $("#MoTa").val();
    $.ajax({
        url: '/SanPhams/EditSanPham',
        data: {
            sanPham: sanPhamModel2,
            ThuocTinhChungs: DanhSachThuocTinhChung,
            ImgsPath: document.getElementById('ImgView').src
        },
        type: "POST",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            } else {
                toastr.success(response);
                HideModal();
                OnLoadSanPham();
            }
        },
        error: function () {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
        }
    })
}


function XoaSanPham(idItem) {
    swal({
        title: "Bạn có chắc chắn muốn xóa?",
        text: "Hành động này sẽ không thể hoàn tác!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/SanPhams/DeleteSanPham',
                data: {
                    id: idItem
                },
                type: "POST",
                success: function (response) {
                    if (response == null || response == undefined || response.length == 0) {
                        swal("Oops!", "Đã xảy ra lỗi!", "error");
                    } else {
                        toastr.success(response);
                        HideModal();
                        OnLoadSanPham();
                    }
                },
                error: function () {
                    swal("Oops!", "Đã xảy ra lỗi!", "error");
                }
            })
        } else {
            toastr.error("Đã hủy xóa", "Thông báo");
        }
    });
}



function getListThuocTinhDaiDien() {
    var list = [];

    $("#ListThuocTinh").find(".accordion-item").each(function () {
        var thuocTinh = {
            TenThuocTinh: $(this).find(".accordion-button").text(),
            giaTriThuocTinhs: []
        };

        $(this).find(".btn-primary-faded").each(function () {
            var giaTriThuocTinh = {
                ID: 0,
                GiaTri: $(this).text().slice(0, -1)
            }
            thuocTinh.giaTriThuocTinhs.push(giaTriThuocTinh);
        });

        list.push(thuocTinh);
    });

    return list;
}


function addThuocTinh() {
    var tenThuocTinh = $("#TenThuocTinh").val().trim(); // Lấy giá trị của #TenThuocTinh bằng jQuery
    if (tenThuocTinh === "") {
        swal("Vui lòng nhập tên thuộc tính.");
        return;
    }

    var accordionValues = $("<ul>").addClass("nav mt-3");

    var accordionBody = $("<div>").addClass("accordion-body");
    var input = $("<input>").attr("type", "text").addClass("form-control").attr("placeholder", "Nhập giá trị");
    var addButton = $("<button>").addClass("btn btn-primary ms-2").text("Thêm");

    addButton.on("click", function () {
        var value = input.val().trim(); // Lấy giá trị từ input
        if (value !== "") {
            var button = $("<button>").addClass("btn btn-primary-faded position-relative border me-3 mt-2").text(value);
            var badge = $("<span>").addClass("position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger").text("X");

            badge.css("cursor", "pointer"); // Đổi con trỏ chuột khi di chuột qua badge

            badge.on("click", function () {
                button.remove();
            });

            button.append(badge);
            accordionValues.append(button);
            input.val("");
        } else {
            swal("Vui lòng nhập giá trị để thêm.");
        }
    });

    var divInput = $("<div>").addClass("d-flex");

    divInput.append(input);
    divInput.append(addButton);
    accordionBody.append(divInput);
    accordionBody.append(accordionValues);

    var accordionHeader = $("<h2>").addClass("accordion-header").append(
        $("<button>").addClass("accordion-button").attr("type", "button").attr("data-bs-toggle", "collapse").attr("data-bs-target", "#collapse" + tenThuocTinh).text(tenThuocTinh)
    );

    var accordionCollapse = $("<div>").addClass("accordion-collapse collapse").attr("id", "collapse" + tenThuocTinh).attr("data-bs-parent", "#ListThuocTinh");
    accordionCollapse.append(accordionBody);

    var accordionItem = $("<div>").addClass("accordion-item");
    accordionItem.addClass("mt-2");
    var badge2 = $("<span>").addClass("position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger").text("X");

    badge2.css("cursor", "pointer"); // Đổi con trỏ chuột khi di chuột qua badge
    badge2.css("z-index", "10"); // Đổi con trỏ chuột khi di chuột qua badge
    badge2.on("click", function () {
        accordionItem.remove()
    });

    accordionItem.append(badge2);
    accordionItem.append(accordionHeader);
    accordionItem.append(accordionCollapse);

    $("#ListThuocTinh").append(accordionItem); // Thêm accordionItem vào #ListThuocTinh

    // Xóa nội dung của input sau khi thêm thành công
    $("#TenThuocTinh").val("");
}



function addThuocTinh2(tenThuocTinh, listGiatri) {

    var accordionValues = $("<ul>").addClass("nav mt-3");

    var accordionBody = $("<div>").addClass("accordion-body");
    var input = $("<input>").attr("type", "text").addClass("form-control").attr("placeholder", "Nhập giá trị");
    var addButton = $("<button>").addClass("btn btn-primary ms-2").text("Thêm");
    addButton.on("click", function () {
        var value = input.val().trim(); // Lấy giá trị từ input
        if (value !== "") {
            var button = $("<button>").addClass("btn btn-primary-faded position-relative border me-3 mt-2").text(value);
            var badge = $("<span>").addClass("position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger").text("X");

            badge.css("cursor", "pointer"); // Đổi con trỏ chuột khi di chuột qua badge

            badge.on("click", function () {
                button.remove();
            });

            button.append(badge);
            accordionValues.append(button);
            input.val("");
        } else {
            swal("Vui lòng nhập giá trị để thêm.");
        }
    });

    listGiatri.forEach(addGiatri);
    function addGiatri(item) {
        var value = item.giaTri;
        var button = $("<button>").addClass("btn btn-primary-faded position-relative border me-3 mt-2").text(value);
        var badge = $("<span>").addClass("position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger").text("X");

        badge.css("cursor", "pointer"); // Đổi con trỏ chuột khi di chuột qua badge

        badge.on("click", function () {
            button.remove();
        });

        button.append(badge);
        accordionValues.append(button);
    };

    var divInput = $("<div>").addClass("d-flex");

    divInput.append(input);
    divInput.append(addButton);
    accordionBody.append(divInput);
    accordionBody.append(accordionValues);

    var accordionHeader = $("<h2>").addClass("accordion-header").append(
        $("<button>").addClass("accordion-button").attr("type", "button").attr("data-bs-toggle", "collapse").attr("data-bs-target", "#collapse" + tenThuocTinh).text(tenThuocTinh)
    );

    var accordionCollapse = $("<div>").addClass("accordion-collapse collapse").attr("id", "collapse" + tenThuocTinh).attr("data-bs-parent", "#ListThuocTinh");
    accordionCollapse.append(accordionBody);

    var accordionItem = $("<div>").addClass("accordion-item position-relative");
    accordionItem.addClass("mt-2");
    var badge2 = $("<span>").addClass("position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger").text("X");

    badge2.css("cursor", "pointer"); // Đổi con trỏ chuột khi di chuột qua badge
    badge2.css("z-index", "10"); // Đổi con trỏ chuột khi di chuột qua badge
    badge2.on("click", function () {
        accordionItem.remove()
    });

    accordionItem.append(accordionHeader);
    accordionItem.append(accordionCollapse);
    accordionItem.append(badge2);
    $("#ListThuocTinh").append(accordionItem);
}

async function ThemSanPham() {
    $("#ThemThuocTinh").css("display", "block");
    $("#SuaThuocTinh").css("display", "none");
    $("#MainModalLabel").html("Thêm sản phẩm");
    $("#ListThuocTinh").html("");
    $("#ID").val(null);
    $("#Name").val(null);
    $("#GiaGoc").val(null);
    $("#GiaGiamGia").val(null);
    $("#SoLuong").val(null);
    $("#State").val(2);
    $("#MoTa").val(null);
    $("#ListThuocTinh").html("");
    document.getElementById('ImgView').src = "";
}


function SuaSanPham(idItem) {
    sanPhamModel = {};
    $("#MainModalLabel").html("Sửa sản phẩm");
    $("#ListThuocTinh").html("");
    $("#ThemThuocTinh").css("display", "none");
    $("#SuaThuocTinh").css("display", "block");

    $.ajax({
        url: '/SanPhams/GetSanPham',
        data: {
            id: idItem
        },
        type: "GET",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            } else {
                sanPhamModel = response;
                if (sanPhamModel == null || sanPhamModel.id == null) {
                    swal("Oops!", "Đã xảy ra lỗi!", "error");
                    return;
                }
                $("#ID").val(sanPhamModel.id);
                $("#Name").val(sanPhamModel.name);
                $("#GiaGoc").val(sanPhamModel.giaGoc);
                $("#GiaGiamGia").val(sanPhamModel.giaGiamGia);
                $("#SoLuong").val(sanPhamModel.soLuong);
                $("#MoTa").val(sanPhamModel.moTa);
                document.getElementById('ImgView').src = sanPhamModel.anhs.img;
                console.log(sanPhamModel.anhs);
                sanPhamModel.thuocTinhs.forEach(addTC);
                function addTC(item) {
                    addThuocTinh2(item.thuocTinhChung.tenThuocTinh, item.thuocTinhChung.giaTriThuocTinhs);
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

function UpdateImage() {
    const file = $("#imgItem").prop('files')[0];
    const reader = new FileReader();
    reader.onloadend = () => {
        const base64String = reader.result;
        document.getElementById('ImgView').src = base64String;
    };
    reader.readAsDataURL(file);
}

