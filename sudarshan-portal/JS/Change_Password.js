function checkPassword()
{
    var password = $("#txt_Old").val();
    var oldpassword = $("#txt_password").val();

    if (password != oldpassword) {
            $("#new_pass").hide();
            $("#comfirm_pass").hide();
            $("#btn_pass").hide();
            $("#Oldmsg").html("Incorrect Old Password");
        }
        else {
            $("#new_pass").show();
            $("#comfirm_pass").show();
            $("#btn_pass").show();
            $("#Oldmsg").html("");
    }
    $("#txt_New").val("");
    $("#txt_Confirm").val("");
    $("#lblNew").html("");
    $("#lblConf").html("");

    $("#btnUpdate").attr("disabled", true);
}
function chkNewPassword() {
    var newp = $("#txt_New").val();
    var conp = $("#txt_Confirm").val();

    if (newp == "") {
        $("#lblNew").html("Enter New Password");
        $("#btnUpdate").attr("disabled", true);
        return false;
    }
    else {
        $("#lblNew").html("");
        if (newp != conp) {
            $("#lblConf").html("Password Does Not Match");
            $("#btnUpdate").attr("disabled", true);
            return false;
        }
        else {
            $("#lblConf").html("");
            $("#btnUpdate").attr("disabled", false);
            return true;
        }
    }
}

function PasswordValidation() {
    var newp = $("#txt_New").val();
    var conp = $("#txt_Confirm").val();
    var password = $("#txt_Old").val();

    if (password != "") {
        $("#Oldmsg").html("");
        if (newp == "") {
            $("#lblNew").html("Enter New Password");
            return false;
        }
        else {
            $("#lblNew").html("");
            if (newp != conp) {
                $("#lblConf").html("Password Does Not Match");
                return false;
            }
            else {
                $("#lblConf").html("");
                return true;
            }
        }
    }
    else {
        $("#Oldmsg").html("Incorrect Old Password");
        return false;
    }
}






