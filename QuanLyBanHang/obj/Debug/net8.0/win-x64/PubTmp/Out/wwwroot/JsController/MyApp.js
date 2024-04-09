var myApp = angular.module('shykeApp', ['ngRoute']);


myApp.run(function ($rootScope, $location) {
    $rootScope.UserInfo = {};
    console.log("loging in...");
    $rootScope.GetUser = function () {
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
                    $rootScope.UserInfo = response;
                    if (response == false) {
                        $("#User_Name").html("Người dùng");
                        $("#User_Role").addClass(Role[1].bg);
                        $("#User_Role").addClass(Role[1].color);
                        $("#User_Role").html(Role[1].name);
                        $("#User_UserName").html("Vui lòng đăng nhập");
                        $("#BtnLogIn").css("display", "");
                        $("#BtnLogOut").css("display", "none");
                    } else {
                        $("#User_Name").html(response.name);
                        $("#User_Role").addClass(Role[Number(response.role)].bg);
                        $("#User_Role").addClass(Role[Number(response.role)].color);
                        $("#User_Role").html(Role[Number(response.role)].name);
                        $("#User_UserName").html("Username: " + response.userName);
                        $("#BtnLogIn").css("display", "none");
                        $("#BtnLogOut").css("display", "");
                    }
                }
            },
            error: function () {
                setTimeout(() => GetUser(), 1500)
            }
        })
    }

    $rootScope.GetUser()

});



function UpdateImage() {
    const file = $("#imgItem").prop('files')[0];
    const reader = new FileReader();
    reader.onloadend = () => {
        const base64String = reader.result;
        document.getElementById('ImgView').src = base64String;
    };
    reader.readAsDataURL(file);
}

function formatDate(date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var day = date.getDate().toString().padStart(2, '0');
    var formattedDate = year + '-' + month + '-' + day;
    return formattedDate;
}