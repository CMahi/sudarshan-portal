<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login Page</title>
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/style.min.css" rel="stylesheet" />
    <style>
        .jssora02r, .jssora02l {
            display: block;
            position: absolute;
            width: 55px;
            height: 55px;
            cursor: pointer;
            background: url('img/a02.png') no-repeat;
            overflow: hidden;
        }

        .jssora02l {
            background-position: -123px -33px;
        }

        .jssora02r {
            background-position: -183px -33px;
        }

        .jssora02l:hover {
            background-position: -3px -33px;
        }

        .jssora02r:hover {
            background-position: -63px -33px;
        }

        .login_box {
            width: 370px;
            height: 400px;
            display: block;
            top: 10px;
            position: relative;
            left: 50%;
            margin-left: -123px;
            z-index: 1020;
            background-color: #A7A9AC;
            border-radius: 5px;
        }

        .new_login {
            width: 320px;
            display: block;
            top: 33px;
            position: relative;
            left: 53%;
            margin-left: -171px;
            z-index: 1020;
            border: 1px solid #5F5E5F;
            border-radius: 5px;
        }
    </style>
</head>

<body style="overflow-x: hidden;">
    <div style="height: 70px; background: #ffffff;">
        <div style="height: 75px; padding-top: 15px; padding-left: 40px; float: left;">
            <img src="Img/133649.png" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-7" style="padding-top: 40px; border-radius: 5px;">
            <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 800px; height: 450px; overflow: hidden; visibility: hidden; border-radius: 10px" runat="server">
                <div id="slider" data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 800px; height: 450px; overflow: hidden;" runat="server">
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/VRM.png" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/4.jpg" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/5.png" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/9.png" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/8.jpg" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/7.jpg" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/2.jpg" />
                    </div>
                    <div data-p="112.50" style="display: none;">
                        <img data-u="image" src="img/1.jpg" />
                    </div>
                </div>
                <span data-u="arrowleft" class="jssora02l" style="top: 0px; left: 8px; width: 55px; height: 55px;" data-autocenter="2"></span>
                <span data-u="arrowright" class="jssora02r" style="top: 0px; right: 8px; width: 55px; height: 55px;" data-autocenter="2"></span>
            </div>
        </div>
        <div class="col-md-3">
            <div style="margin-top: 25px; margin-left: 40px;">
                <%--<img src="images/Untitled.png" />--%>
            </div>
            <div class="login_box">
                <div class="new_login">
                    <div class="login-brand text-white" style="background-color: #DD463B; text-align: center; border-bottom: 1px solid #5F5E5F;">Login Page</div>
                    <div class="login-content">
                        <form action="Authenticate.aspx" method="post" name="login_form" class="form-input-flat">
                            <div class="form-group">
                                <div class="input-group m-b-15">
                                    <span class="input-group-addon"><i class="fa fa-fw fa-user"></i></span>
                                    <input id="txtlogin" runat="server" type="text" class="form-control" placeholder="User Name" style="border-width: 1px; border-color: #000000;" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="input-group m-b-15">
                                    <span class="input-group-addon"><i class="fa fa-fw fa-key"></i></span>
                                    <input id="txtpassword" runat="server" type="password" class="form-control" placeholder="Password" style="border-width: 1px; border-color: #000000;" />
                                </div>
                            </div>
                            <div class="row m-b-20">
                                <div class="col-md-12">
                                    <button type="submit" class="btn btn-danger btn-block">Login</button>
                                </div>
                            </div>
                            <div id="alert_message" runat="server" class="alert-danger">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                    &times;</button>
                                <label id="errMsg" class="alert-error" runat="server" />
                            </div>
                            <div class="text-center m-t-10">
                                <img src="images/forgotpassword.png" /><a href="Forgot_Password.aspx" class="text-muted">Forgot Password?</a>
                            </div>
                            <div class="text-center m-t-10">
                                <img src="images/faq1.png" /><a href="#modal-dialog" data-toggle="modal"><b>FAQ's</b></a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modal-dialog" style="margin-top: 50px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-fw pull-left fa-lg fa-times-circle-o"></i></button>
                    <h4 class="modal-title"><b>Frequently Asked Questions</b></h4>
                </div>
                <div class="modal-body" style="height: 350px; overflow-y: scroll">
                    <div class="col-md-12">
                        <div class="panel-group" id="accordion">
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                            <i class="fa fa-plus-circle pull-left"></i>
                                            What should I do in case I forget my password?
									    </a>
                                    </h3>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        1 Click on link "Forgot Password"<br />
                                        2 You have to enter userid and Emailid which is regiseterd with SCIL.<br />
                                        3 If both Username and Email Id are correct then password will be sent to your registered emailid.<br />
                                    </div>
                                </div>
                            </div>
			    <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                                            <i class="fa fa-plus-circle pull-left"></i>
                                            Whom to contact for vendor portal issues?
									    </a>
                                    </h3>
                                </div>
                                <div id="collapseThree" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        Kindly send detail Email on "support@sudarshan.com"                             
								   
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                                            <i class="fa fa-plus-circle pull-left"></i>
                                            How to change my password?
									    </a>
                                    </h3>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        1 Once you login to system click on arrow near your name.<br />
                                        2 Click on change password.<br />
                                        3 Enter your old and new password.<br />

                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                                            <i class="fa fa-plus-circle pull-left"></i>
                                            What do I do if I get message 'Invalid user or password'?
									    </a>
                                    </h3>
                                </div>
                                <div id="collapseThree" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        1 Check username and password are correct or not
                             
								   
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                                            <i class="fa fa-plus-circle pull-left"></i>
                                            What do I do if I get the message 'Your login is blocked please contact Vendor Portal Admin'?
									    </a>
                                    </h3>
                                </div>
                                <div id="collapseFour" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        You  should contact Portal Admin to get it unlocked
								   
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFive">
                                            <i class="fa fa-plus-circle pull-left"></i>
                                            Does my password expire periodically?
									    </a>
                                    </h3>
                                </div>
                                <div id="collapseFive" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        No your password is not at all expired periodically.
								   
                                    </div>
                                </div>
                            </div>
			      <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSix">
                                            <i class="fa fa-plus-circle pull-left"></i>
					     What are the minimum browser requirements for this site?									    </a>
                                    </h3>
                                </div>
                                <div id="collapseSix" class="panel-collapse collapse">
                                    <div class="panel-body" style="background-color: #ebebeb">
                                        1 Internet Explorer 11 & above.<br />
					2 Google Crome 44 & above.
								   
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<div class="modal" id="modal-msg">
        <div class="modal-dialog" style="width: 95%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    
                </div>
                <div class="modal-body">
                  
                   <div class="form-horizontal">
                        <div class="form-group">
                            <label class="control-label col-md-12" style="text-align: center; font-size:20px;">
                                <b> Datacenter maintenance activity Vendor Portal and Expense Reimbursement not available as per below.</b></label>
                            
                        </div>
                        <div class="form-group" style="text-align: center;">
                             <img src="Img/image002.png" height="200px" width="500px" style="margin-top: 1px;"
                    title="Employee Attendance" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12" style="text-align: center;">
                            <a href="javascript:;" class="btn btn-info width-100" data-dismiss="modal">
                                OK</a>
                        </div>
                    </div>

                   
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <script src="assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/jssor.slider.mini.js"></script>

    <script type="text/javascript">
$(document).ready(function () {
           // $("#modal-msg").modal("show");

        });
        window.setTimeout(function () {
            $("#alert_message").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 3000);


        jQuery(document).ready(function ($) {

            var jssor_1_SlideoTransitions = [
              [{ b: 0, d: 600, y: -290, e: { y: 27 } }],
              [{ b: 0, d: 1000, y: 185 }, { b: 1000, d: 500, o: -1 }, { b: 1500, d: 500, o: 1 }, { b: 2000, d: 1500, r: 360 }, { b: 3500, d: 1000, rX: 30 }, { b: 4500, d: 500, rX: -30 }, { b: 5000, d: 1000, rY: 30 }, { b: 6000, d: 500, rY: -30 }, { b: 6500, d: 500, sX: 1 }, { b: 7000, d: 500, sX: -1 }, { b: 7500, d: 500, sY: 1 }, { b: 8000, d: 500, sY: -1 }, { b: 8500, d: 500, kX: 30 }, { b: 9000, d: 500, kX: -30 }, { b: 9500, d: 500, kY: 30 }, { b: 10000, d: 500, kY: -30 }, { b: 10500, d: 500, c: { x: 87.50, t: -87.50 } }, { b: 11000, d: 500, c: { x: -87.50, t: 87.50 } }],
              [{ b: 0, d: 600, x: 410, e: { x: 27 } }],
              [{ b: -1, d: 1, o: -1 }, { b: 0, d: 600, o: 1, e: { o: 5 } }],
              [{ b: -1, d: 1, c: { x: 175.0, t: -175.0 } }, { b: 0, d: 800, c: { x: -175.0, t: 175.0 }, e: { c: { x: 7, t: 7 } } }],
              [{ b: -1, d: 1, o: -1 }, { b: 0, d: 600, x: -570, o: 1, e: { x: 6 } }],
              [{ b: -1, d: 1, o: -1, r: -180 }, { b: 0, d: 800, o: 1, r: 180, e: { r: 7 } }],
              [{ b: 0, d: 1000, y: 80, e: { y: 24 } }, { b: 1000, d: 1100, x: 570, y: 170, o: -1, r: 30, sX: 9, sY: 9, e: { x: 2, y: 6, r: 1, sX: 5, sY: 5 } }],
              [{ b: 2000, d: 600, rY: 30 }],
              [{ b: 0, d: 500, x: -105 }, { b: 500, d: 500, x: 230 }, { b: 1000, d: 500, y: -120 }, { b: 1500, d: 500, x: -70, y: 120 }, { b: 2600, d: 500, y: -80 }, { b: 3100, d: 900, y: 160, e: { y: 24 } }],
              [{ b: 0, d: 1000, o: -0.4, rX: 2, rY: 1 }, { b: 1000, d: 1000, rY: 1 }, { b: 2000, d: 1000, rX: -1 }, { b: 3000, d: 1000, rY: -1 }, { b: 4000, d: 1000, o: 0.4, rX: -1, rY: -1 }]
            ];

            var jssor_1_options = {
                $AutoPlay: true,
                $Idle: 2000,
                $CaptionSliderOptions: {
                    $Class: $JssorCaptionSlideo$,
                    $Transitions: jssor_1_SlideoTransitions,
                    $Breaks: [
                      [{ d: 2000, b: 1000 }]
                    ]
                },
                $ArrowNavigatorOptions: {
                    $Class: $JssorArrowNavigator$
                },
                $BulletNavigatorOptions: {
                    $Class: $JssorBulletNavigator$
                }
            };

            var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);
            function ScaleSlider() {
                var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
                if (refSize) {
                    refSize = Math.min(refSize, 800);
                    jssor_1_slider.$ScaleWidth(refSize);
                }
                else {
                    window.setTimeout(ScaleSlider, 30);
                }
            }
            ScaleSlider();
            $(window).bind("load", ScaleSlider);
            $(window).bind("resize", ScaleSlider);
            $(window).bind("orientationchange", ScaleSlider);
        });

    </script>
</body>
</html>

