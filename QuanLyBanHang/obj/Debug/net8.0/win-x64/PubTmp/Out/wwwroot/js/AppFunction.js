toastr.options.progressBar = true;
toastr.options.closeButton = true;
toastr.options.closeHtml = '<button><i class="icon-off"></i></button>';
toastr.options.timeOut = 3000;

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function deleteCookies() {
    var Cookies = document.cookie.split(';');

    // set 1 Jan, 1970 expiry for every cookies
    for (var i = 0; i < Cookies.length; i++)
        document.cookie = Cookies[i] + "=;expires=" + new Date(0).toUTCString();
}
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
    deleteCookies();
    setTimeout(() => {
        document.location.href = "/"
    }, 500);
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


