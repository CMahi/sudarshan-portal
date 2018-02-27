<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Video.aspx.cs" Inherits="Video" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />

    <link href="CSS/jPlayer.css" rel="stylesheet" />
    <link href="CSS/prettify-jPlayer.css" rel="stylesheet" />
    <link href="CSS/jplayer.pink.flag.min.css" rel="stylesheet" />

    <script type="text/javascript" src="JS/jquery.min.js"></script>
    <script type="text/javascript" src="JS/jquery.jplayer.min.js"></script>
    <script type="text/javascript" src="JS/jplayer.playlist.min.js"></script>
    <script type="text/javascript" src="JS/jquery.jplayer.inspector.min.js"></script>
    <script type="text/javascript" src="JS/themeswitcher.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            new jPlayerPlaylist({
                jPlayer: "#jquery_jplayer_1",
                cssSelectorAncestor: "#jp_container_1"
            }, [
                {
                    title: "Acknowledge PO.mp4",
                    artist: "Blender Foundation",
                    free: true,
                    m4v: "VP_Videos/Acknowledge_PO.mp4",

                },
                {
                    title: "Dispatch Note.mp4",
                    artist: "Pixar",
                    m4v: "VP_Videos/Dispatch_Note.mp4",

                },
                {
                    title: "Early Payment Request.mp4",
                    artist: "Pixar",
                    m4v: "VP_Videos/Early_Payment_Request.mp4",

                },
                {
                    title: "Reports.mp4",
                    artist: "Pixar",
                    m4v: "VP_Videos/Reports.mp4",

                },
                {
                    title: "Vendor Login.mp4",
                    artist: "Pixar",
                    m4v: "VP_Videos/Vendor_Login.mp4",

                },
                {
                    title: "Vendor Login forgot passowrd.mp4",
                    artist: "Pixar",
                    m4v: "VP_Videos/Vendor_Login_forgot_passowrd.mp4",

                }

            ], {
                swfPath: "../dist/jplayer",
                supplied: "webmv, ogv, m4v",
                useStateClassSkin: true,
                autoBlur: false,
                smoothPlayBar: true,
                keyEnabled: true
            });
            $("#jplayer_inspector_1").jPlayerInspector({ jPlayer: $("#jquery_jplayer_1") });
            $("#jplayer_inspector_2").jPlayerInspector({ jPlayer: $("#jquery_jplayer_2") });
        });

    </script>
</head>
<body class="demo" onload="prettyPrint();">
    <div id="container">
        <div id="content_main">
            <div id="jp_container_1" class="jp-video jp-video-270p" role="application" aria-label="media player">
                <div class="jp-type-playlist">
                    <div id="jquery_jplayer_1" class="jp-jplayer"></div>
                    <div class="jp-gui">
                        <div class="jp-video-play">
                            <button class="jp-video-play-icon" role="button" tabindex="0">play</button>
                        </div>
                        <div class="jp-interface">
                            <div class="jp-progress">
                                <div class="jp-seek-bar">
                                    <div class="jp-play-bar"></div>
                                </div>
                            </div>
                            <div class="jp-current-time" role="timer" aria-label="time">&nbsp;</div>
                            <div class="jp-duration" role="timer" aria-label="duration">&nbsp;</div>
                            <div class="jp-details">
                                <div class="jp-title" aria-label="title">&nbsp;</div>
                            </div>
                            <div class="jp-controls-holder">
                                <div class="jp-volume-controls">
                                    <button class="jp-mute" role="button" tabindex="0">mute</button>
                                    <button class="jp-volume-max" role="button" tabindex="0">max volume</button>
                                    <div class="jp-volume-bar">
                                        <div class="jp-volume-bar-value"></div>
                                    </div>
                                </div>
                                <div class="jp-controls">
                                    <button class="jp-previous" role="button" tabindex="0">previous</button>
                                    <button class="jp-play" role="button" tabindex="0">play</button>
                                    <button class="jp-stop" role="button" tabindex="0">stop</button>
                                    <button class="jp-next" role="button" tabindex="0">next</button>
                                </div>
                                <div class="jp-toggles">
                                    <button class="jp-repeat" role="button" tabindex="0">repeat</button>
                                    <button class="jp-shuffle" role="button" tabindex="0">shuffle</button>
                                    <button class="jp-full-screen" role="button" tabindex="0">full screen</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="jp-playlist">
                        <ul>
                            <li></li>
                            s
                        </ul>
                    </div>

                </div>
            </div>
        </div>
    </div>
</body>
<script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>
<script src="JS/prettify-jPlayer.js"></script>
</html>
