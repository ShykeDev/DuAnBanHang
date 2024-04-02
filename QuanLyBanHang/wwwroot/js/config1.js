toastr.options.progressBar = true;
toastr.options.closeButton = true;
toastr.options.closeHtml = '<button><i class="icon-off"></i></button>';
toastr.options.timeOut = 3000;

function setCookie(cname, cvalue) {
    document.cookie = cname + "=" + cvalue
}

function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function LogOut() {
    setCookie("ID", "");
    setCookie("Name", "");
    setCookie("UserName", "");
    setCookie("Password", "");
    setCookie("Role", "");
    document.location.href = "/"
}

var Role = [
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


GetUser()
function GetUser() {
    $.ajax({
        url: '/Home/GetUser',
        data: {
            id: getCookie("ID"),
        },
        type: "GET",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                swal("Oops!", "Đã xảy ra lỗi!", "error");
            } else {
                console.log(response);
                if (response == false) {
                    $("#User_Name").html("Người dùng");
                    $("#User_Role").addClass(Role[1].bg);
                    $("#User_Role").addClass(Role[1].color);
                    $("#User_Role").html(Role[1].name);
                    $("#User_UserName").html("Vui lòng đăng nhập");
                    $("#BtnLogIn").css("display", "");
                    $("#BtnLogOut").css("display", "none");
                    $("#AdminLi").css("display", "none");
                    $("#UserLi").css("display", "");
                } else {
                    $("#User_Name").html(response.name);
                    $("#User_Role").addClass(Role[Number(response.role)].bg);
                    $("#User_Role").addClass(Role[Number(response.role)].color);
                    $("#User_Role").html(Role[Number(response.role)].name);
                    $("#User_UserName").html("Username: " + response.userName);
                    $("#BtnLogIn").css("display", "none");
                    $("#BtnLogOut").css("display", "");
                    if (response.role == 0) {
                        $("#AdminLi").css("display", "");
                        $("#UserLi").css("display", "none");
                    } else {
                        $("#AdminLi").css("display", "none");
                        $("#UserLi").css("display", "");
                    }
                }
            }
        },
        error: function () {
            swal("Oops!", "Đã xảy ra lỗi!", "error");
        }
    })
}



$(document).ready(function () {
    $("#searchText").change(function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchText").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchText").on("input", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});


