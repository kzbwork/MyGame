﻿var login_ok = false;
var type = null;
var can = false;
var chessArray = new Array();
ininarray()
var start = function () {
    var wsImpl = window.WebSocket || window.MozWebSocket;
    window.ws = new wsImpl('ws://192.168.157.166:7181/');
    // when data is comming from the server, this metod is called
    ws.onmessage = function (evt) {
        if (evt.data == "game start")
        {
            get_duishou();
            ininarray();
            return;
        }

        var data = eval('(' + evt.data + ')');
        if (data.status == "ok" || data.status == "false")
        {
            var a = data.msg;
            var outx = a.x;
            var outy = a.y;
            var is_win = a.is_win;
            if (a.type != type)
            {
                dian(outx * 40, outy * 40, false);
                chessArray[outx][outy] = a.type;
            }
            setTimeout(function () {
                if (is_win == "true") {
                    if (a.type == type) {
                        alert("你赢了");
                    }
                    else {
                        alert("你输了");
                    }
                    setTimeout(function () {
                        $("#qipan").html("");
                        get_duishou();
                        ininarray();
                        loadqipan();
                    }, 3000);
                }
                else {
                    if (a.type == type) {
                        can = false;
                    }
                    else {
                        can = true;
                    }
                }
            }, 1000);
        }
    };

    // when the connection is established, this method is called
    ws.onopen = function (a) {
    };

    // when the connection is closed, this method is called
    ws.onclose = function () {

    }

    $("#send").on("click", function (e) {
        e.preventDefault();
        var val = input.value;
        ws.send(val);
        input.value = "";
    });
}

$(function () {
    //login();
    //loadqipan();
    //$.ajax({
    //    type: "POST",
    //    datatype: "json",
    //    url: "http://" + document.domain + ":12606/handler/WuziHnadler.ashx?action=join",
    //    success: function (d) {
    //        //start();
    //        alert(d.status);
    //    }
    //});
});

function get_duishou(a)
{
    if (a == null || a == undefined)
    {
        a == ""
    }
    $.ajax({
        type: "POST",
        datatype: "json",
        url: "http://" + document.domain + ":12606/handler/WuziHnadler.ashx?action=getduishou&re="+a,
        success: function (data) {
            data = eval('(' + data + ')');
            if (data.status == "ok") {
                var state = data.msg.type;
                if (type == "2") {
                    var ip = data.msg.ip;
                    var winnum = data.msg.winnum;
                    var lesstime = data.msg.lesstime;
                    var choose = data.msg.choose;
                    $("#player1").html(ip);
                    $("#player1_time").html(lesstime);
                    $("#player1_win").html(winnum);
                    $("#join1").hide();
                    can = true;
                }
                else {
                    var ip = data.msg.ip;
                    var winnum = data.msg.winnum;
                    var lesstime = data.msg.lesstime;
                    var choose = data.msg.choose;
                    $("#player2").html(ip);
                    $("#player2_time").html(lesstime);
                    $("#player2_win").html(winnum);
                    $("#join2").hide();
                }
            }
        }
    });
}

function login() {
    $.ajax({
        type: "POST",
        datatype: "json",
        url: "http://" + document.domain + ":12606/handler/WuziHnadler.ashx?action=join",
        success: function (data) {
            data = eval('(' + data + ')');
            if (data.status == "ok")
            {
                var state = data.msg.type;
                if (state == "frist")
                {
                    var ip = data.msg.ip;
                    var winnum = data.msg.winnum;
                    var lesstime = data.msg.lesstime;
                    var choose = data.msg.choose;
                    $("#player1").html(ip);
                    $("#player1_time").html(lesstime);
                    $("#player1_win").html(winnum);
                    type = parseInt(choose);
                    $("#join1").hide();
                    loadqipan();
                }

                if (state == "second") {
                    var ip = data.msg.ip;
                    var winnum = data.msg.winnum;
                    var lesstime = data.msg.lesstime;
                    var choose = data.msg.choose;
                    $("#player2").html(ip);
                    $("#player2_time").html(lesstime);
                    $("#player2_win").html(winnum);
                    type = parseInt(choose);
                    $("#join2").hide();
                    loadqipan();
                }
                start();
            }
        }
    });
}

function loadqipan() {
    var basex = 40;
    var basey = 40;
    var img = null;
    for (var i = 0 ; i < 15 ; i++) {
        for (var j = 0; j < 15; j++) {
            if (i == 0 && j == 0) {
                img = '../source/qi_part_left_top.png';//左上角
            }
            else if (i > 0 && i < 14 && j == 0) {
                img = '../source/qi_part_top_bian.png';//上边
            }
            else if (i == 14 && j == 0) {
                img = '../source/qi_part_right_top.png';//右上角
            }
            else if (i == 0 && j > 0 && j < 14) {
                img = '../source/qi_part_left_bian.png';//左边
            }
            else if (i == 0 && j == 14) {
                img = '../source/qi_part_left_bot.png';//左下角
            }
            else if (j == 14 && i > 0 && i < 14) {
                img = '../source/qi_part_bot_bian.png';//下边
            }
            else if (i == 14 && j == 14) {
                img = '../source/qi_part_right_bot.png';//右下角
            }
            else if (i == 14 && j > 0 && j < 14) {
                img = '../source/qi_part_right_bian.png';//右边
            }
            else if ((i == 3 && j == 3) || (i == 7 && j == 3) || (i == 11 && j == 3) || (i == 3 && j == 7) || (i == 7 && j == 7) || (i == 11 && j == 7) || (i == 3 && j == 11) || (i == 7 && j == 11) || (i == 11 && j == 11)) {
                img = '../source/qi_part_dian.png';//特殊点
            }
            else {
                img = '../source/qi_part.png';//普通点
            }
            $('<div style="position: absolute; left: ' + 40 * i + 'px; top: ' + 40 * j + 'px; background-image: url(\'' + img + '\');z-index:2;width:40px;height:40px;" onclick="dian(' + 40 * i + ',' + 40 * j + ',true)"></div>').appendTo($("#qipan"));
        }
    }
}

function dian(x, y, z) {
    if (can == false && z == true) {
        return;
    }
    var yanse = null;
    if (z == false) {
        if (type == 1) {
            yanse = 2;
        }
        else {
            yanse = 1;
        }
    }
    else {
        yanse = type;
    }
    var index_x = null;
    var index_y = null;
    index_x = x / 40;
    index_y = y / 40;
    if (chessArray[index_x][index_y] == 1 || chessArray[index_x][index_y] == 2) {
        return;
    }
    if (yanse == 2) {
        $('<div style="position: absolute; left: ' + (parseInt(x) + parseInt(5)) + 'px; top: ' + (parseInt(y) + parseInt(5)) + 'px; background-color:#000;z-index:3;width:30px;height:30px;border-radius:30px;box-shadow:1px 1px 1px #aaaaaa;""></div>').appendTo($("#qipan"));
        if (z == true)
        {
            chessArray[index_x][index_y] = 2;
        } 
    }
    else {
        $('<div style="position: absolute; left: ' + (parseInt(x) + parseInt(5)) + 'px; top: ' + (parseInt(y) + parseInt(5)) + 'px; background-color:#fff;z-index:3;width:30px;height:30px;border-radius:30px;box-shadow:1px 1px 1px #aaaaaa;""></div>').appendTo($("#qipan"));
        if (z == true) {
            chessArray[index_x][index_y] = 1;
        }
    }
    var o = '{ "task": "down", "x": "' + index_x + '", "y": "' + index_y + '" ,"type":"' + type + '"}';
    if (z == true)
    {
        window.ws.send(o);
    }
}

function ininarray()
{
    //0是黑子 1是白子
    for (var k = 0; k < 15; k++) {

        chessArray[k] = new Array();

        for (var j = 0; j < 15; j++) {
            chessArray[k][j] = "z";
        }
    }
}