var chessArray = new Array();
$(function () {
    loadqipan();
    //0是黑子 1是白子
    for (var k = 0; k < 15; k++) {

        chessArray[k] = new Array();

        for (var j = 0; j < 15; j++) {
            chessArray[k][j] = "z";
        }
    }
})
function loadqipan()
{
    var basex = 40;
    var basey = 40;
    var img = null;
    for(var i = 0 ;i < 15 ;i++)
    {
        for(var j = 0;j < 15;j++)
        {
            if(i == 0 && j == 0)
            {
                img = 'source/qi_part_left_top.png';//左上角
            }
            else if(i > 0 && i < 14 && j==0)
            {
                img = 'source/qi_part_top_bian.png';//上边
            }
            else if(i == 14 && j == 0)
            {
                img ='source/qi_part_right_top.png';//右上角
            }
            else if(i == 0 && j >0 && j < 14)
            {
                img ='source/qi_part_left_bian.png';//左边
            }
            else if(i == 0 && j == 14)
            {
                img = 'source/qi_part_left_bot.png';//左下角
            }
            else if(j == 14 && i > 0 && i < 14)
            {
                img = 'source/qi_part_bot_bian.png';//下边
            }
            else if(i == 14 && j ==14)
            {
                img ='source/qi_part_right_bot.png';//右下角
            }
            else if (i == 14 && j > 0 && j < 14)
            {
                img = 'source/qi_part_right_bian.png';//右边
            }
            else if ((i == 3 && j == 3) || (i == 7 && j == 3) || (i == 11 && j == 3) || (i == 3 && j == 7) || (i == 7 && j == 7) || (i == 11 && j == 7) || (i == 3 && j == 11) || (i == 7 && j == 11) || (i == 11 && j == 11)) {
                img = 'source/qi_part_dian.png';//特殊点
            }
            else {
                img = 'source/qi_part.png';//普通点
            }
            $('<div style="position: absolute; left: ' + 40 * i + 'px; top: ' + 40 * j + 'px; background-image: url(\'' + img + '\');z-index:2;width:40px;height:40px;" onclick="dian(' + 40 * i + ',' + 40 * j + ')"></div>').appendTo($("#qipan"));
        }
    }
}

var step = 0;
function dian(x, y)
{
    var index_x = null;
    var index_y = null;
    index_x = x / 40;
    index_y = y / 40;
    var temp = null;
    if (chessArray[index_x][index_y] == 0 || chessArray[index_x][index_y] == 1)
    {
        return;
    }
    if (step == 0 || step % 2 == 0) {
        $('<div style="position: absolute; left: ' + (parseInt(x) + parseInt(5)) + 'px; top: ' + (parseInt(y) + parseInt(5)) + 'px; background-color:#000;z-index:3;width:30px;height:30px;border-radius:30px;box-shadow:1px 1px 1px #aaaaaa;""></div>').appendTo($("#qipan"));
        chessArray[index_x][index_y] = 0;
        temp = 0;
    }
    else {
        $('<div style="position: absolute; left: ' + (parseInt(x) + parseInt(5)) + 'px; top: ' + (parseInt(y) + parseInt(5)) + 'px; background-color:#fff;z-index:3;width:30px;height:30px;border-radius:30px;box-shadow:1px 1px 1px #aaaaaa;""></div>').appendTo($("#qipan"));
        chessArray[index_x][index_y] = 1;
        temp = 1;
    }
    step++;
    //判断四条线是否五个子 如果是 判断输赢
    var win = 0;
    //横向
    for (var i = 0 ; i < 5 ; i++) {
        win = 0;
        var temp_heng = 5 - i;
        for (var j = temp_heng - 5; j < temp_heng; j++) {
            if (index_x + j >= 0 && index_x + j <= 15) {
                if (chessArray[index_x + j][index_y] == temp) {
                    win++;
                }
            }
        }
        if (win == 5) {
            break;
        }
    }
    if (win == 5) {
        if (temp == 0) {
            alert("黑赢了");
            return;
        }
        else {
            alert("白赢了");
            return;
        }
    }
    else {
        win = 0;
    }
    //竖向
    for (var i = 0 ; i < 5 ; i++) {
        win = 0;
        var temp_heng = 5 - i;
        for (var j = temp_heng - 5; j < temp_heng; j++) {
            if (index_y + j >= 0 && index_y + j <= 15) {
                if (chessArray[index_x][index_y + j] == temp) {
                    win++;
                }
            }
        }
        if (win == 5) {
            break;
        }
    }
    if (win == 5) {
        if (temp == 0) {
            alert("黑赢了");
            return;
        }
        else {
            alert("白赢了");
            return;
        }
    }
    else {
        win = 0;
    }
    //左斜向
    for (var i = 0 ; i < 5 ; i++) {
        win = 0;
        var temp_heng = 5 - i;
        for (var j = temp_heng - 5; j < temp_heng; j++) {
            if (index_y + j >= 0 && index_y + j <= 15 && index_x + j >= 0 && index_x + j <= 15) {
                if (chessArray[index_x + j][index_y + j] == temp) {
                    win++;
                }
            }
        }
        if (win == 5) {
            break;
        }
    }
    if (win == 5) {
        if (temp == 0) {
            alert("黑赢了");
            return;
        }
        else {
            alert("白赢了");
            return;
        }
    }
    else {
        win = 0;
    }
    //右斜向
    for (var i = 0 ; i < 5 ; i++) {
        win = 0;
        var temp_heng = 5 - i;
        for (var j = temp_heng - 5; j < temp_heng; j++) {
            if (index_y - j <= 15 && index_y - j >= 0 && index_x + j >= 0 && index_x + j <= 15) {
                if (chessArray[index_x + j][index_y - j] == temp) {
                    win++;
                }
            }
        }
        if (win == 5) {
            break;
        }
    }
    if (win == 5) {
        if (temp == 0) {
            alert("黑赢了");
            return;
        }
        else {
            alert("白赢了");
            return;
        }
    }
    else {
        win = 0;
    }
}