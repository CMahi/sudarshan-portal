
function getvalidate() {
    var UserName = $("#txt_UserName").val();
    var Email = $("#txt_Email").val();

    if (UserName == "") {
        alert("Enter User Name...!");
        return false;
    }
    else if (Email == "") {
        alert("Enter your email ID...!");
        return false;
    }
}
