"use strict";
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .build();

//file room
connection.on("NewUserJoined", (userName) => {
    // Thêm người dùng mới vào danh sách người dùng và cập nhật giao diện
    const userList = document.getElementById("user-list");
    const listItem = document.createElement("LI");
    listItem.textContent = userName;
    userList.appendChild(listItem);
});

connection.on("navigateToPage", function (pageUrl) {
    // Chuyển hướng tới trang với đường dẫn pageUrl
    window.location.href = pageUrl;
});


//file phongdau
connection.on("UpdateScore1", function (html) {
    $('#playerNscore1').empty();
    $('#playerNscore1').html(html);
});

connection.on("navigateToPage1", function (pageUrl) {
    // Chuyển hướng tới trang với đường dẫn pageUrl
    window.location.href = pageUrl;
});

var goi1 = 0;
var goi2 = 0;

var _canCalla = true;
var _lastCalledTimea = Date.now() - 10000; // set to 10 seconds ago
connection.on("UpdateAnswer", function (html1) {

    var currentTime = Date.now();
    if (_canCalla && (currentTime - _lastCalledTimea) >= 10000) {
        goi1++;
        console.log("Đây là update answer");
        console.log("Số lần update: " + goi1);
        if (goi1 == 4 || goi1 == 8 || goi1 == 12 || goi1 == 16 || goi1 == 20) {
            $('#playerNscore2').empty();
            $('#playerNscore2').html(html1);
        }

        // End of your code
        setTimeout(function () {
            _canCalla = true;
        }, 19500);
    }
});


var _canCallb = true;
var _lastCalledTimeb = Date.now() - 10000; // set to 10 seconds ago
connection.on("UpdateResult", function (html, idcauhoi, result) {

    var currentTime = Date.now();
    if (_canCallb && (currentTime - _lastCalledTimeb) >= 10000) {
        goi2++;
        console.log("Đây là update result");
        console.log("Số lần update: " + goi2);
        if (goi2 == 4 || goi2 == 8 || goi2 == 12 || goi2 == 16 || goi1 == 20) {
            $('#playerNscore2').empty();
            $('#playerNscore2').html(html);
            console.log("Thông báo result signalR " + result);
            $('#hienthidapan').trigger('click', [idcauhoi, result]);
        }

        // End of your code
        setTimeout(function () {
            _canCall = true;
        }, 19500);
    }
});

connection.on("UpdateScore2", function (html) {
    $('#playerNscore2').empty();
    $('#playerNscore2').html(html);


});



var updategoi = 0;
var _canCall = true;
let canCall = true;
var _lastCalledTime = Date.now() - 10000; // set to 10 seconds ago
connection.on("UpdateQuestion", function (cauhoi2, hangngang) {
    if (canCall) {
        canCall = false;
        updategoi++;
        console.log("số lần gọi thời gian:" + updategoi);
        var cauhoiv2 = JSON.parse(cauhoi2);
        var hangngangchon = hangngang - 1;
        var question = cauhoiv2[hangngangchon];

        var idcauhoi = question.CauHoiId;
        $('#hienthiovuong').trigger('click', idcauhoi);

        var theloaicauhoi = "";
        theloaicauhoi += "<p> HÀNG NGANG " + hangngang + " - " + question.LinhVuc.TenLinhVuc.toUpperCase();
        theloaicauhoi += "</p>";
        $('#theloaicauhoi').html(theloaicauhoi);

        var questionHtml = "";
        questionHtml += "<p style=\"text-align: center; margin: 20px; font-family: 'Open Sans', sans-serif; color: white; font-weight: bold; font-size: 15px\">";
        questionHtml += question.NoiDung;
        questionHtml += "</p>";
        $('#question-containers').html(questionHtml);

        var donghocat = document.getElementById("donghocat");
        // Thêm class "column" vào phần tử
        donghocat.classList.add("column");

        console.log(donghocat);
        

        //lấy đáp án

        setTimeout(function () {
            var idchon = 1;
            canCall = true;
            var idcauhoi = parseInt(question.CauHoiId);
            donghocat.classList.remove("column");
            $('#submitButton2').trigger('click', [idchon, idcauhoi]);
        }, 10000);

    }
    


});

connection.on("VoHieuHoaBtn", function (idplayer)
{
    var id = parseInt(idplayer);
    $('#vohieuhoabtn').trigger('click', id);
}
);

var _canCall2 = true;
var _lastCalledTime2 = Date.now() - 10000; // set to 10 seconds ago

connection.on("NextQuestionne", function (solangoi, list, listchch) {

    console.log("Đây là thông báo log");

    console.log("Số lần gọi: " + solangoi);
    var idbiloai = JSON.parse(list);
    var cauhoichuahoi = JSON.parse(listchch);
    console.log("List idbiloai: " + idbiloai);
    if (cauhoichuahoi.length > 0)
    {
        console.log("Những câu chưa hỏi:");
        console.log(cauhoichuahoi);
        if (solangoi < 4) {
            if (idbiloai != null || idbiloai.length > 0) {
                for (var i = 0; i < idbiloai.length; i++) {
                    if (idbiloai[i] == solangoi) {
                        solangoi++;
                    }
                }
            }
            $('#hienthinguoitieptheo').trigger('click', solangoi);
        }
        //else
        //{
        //    //var randomIndex = Math.floor(Math.random() * cauhoichuahoi.length);
        //    //var randomElement = cauhoichuahoi[randomIndex];
        //    /*var idchon = 0;*/
        //    /*var idhangngang = randomElement + 1;*/
        //    var idhangngang = cauhoichuahoi[0] + 1;
        //    /*console.log("random hàng ngang số: " + idhangngang)*/
        //    var currentTime = Date.now();
        //    if (_canCall && (currentTime - _lastCalledTime) >= 10000)
        //    {
        //        setTimeout(function () {
        //            /*donghocat.classList.remove("column");*/
        //            $('#randomcauhoi').trigger('click', idhangngang);
        //        }, 10000);

        //        // End of your code
        //        setTimeout(function () {
        //            _canCall = true;
        //        }, 19500);
        //    }

        //}
        else {
            /*$('#hienthinguoitieptheo').trigger('click', 3);*/
            //var randomIndex = Math.floor(Math.random() * cauhoichuahoi.length);
            //var randomElement = cauhoichuahoi[randomIndex];
            /*var idchon = 0;*/
            /*var idhangngang = randomElement + 1;*/
            var idhangngang = cauhoichuahoi[0] + 1;
            /*console.log("random hàng ngang số: " + idhangngang)*/
            var currentTime = Date.now();
            if (_canCall2 && (currentTime - _lastCalledTime2) >= 10000)
            {
                setTimeout(function () {
                    /*donghocat.classList.remove("column");*/
                    $('#randomcauhoi').trigger('click', idhangngang);
                }, 10000);

                // End of your code
                setTimeout(function () {
                    _canCall = true;
                }, 19500);
            }
        }
    }
    else
    {
        console.log("Đã full câu hỏi");
        const choncauhoiDivs = document.querySelectorAll('.choncauhoi');
        choncauhoiDivs.forEach(div => {
            const children = div.querySelectorAll('*');
            children.forEach(child => {
                child.style.color = 'white';
                child.style.borderColor = 'white';
                child.style.opactity = 0.5;
            });
        });

        var ovuong1 = document.getElementById("ovuong1");
        var ovuong2 = document.getElementById("ovuong2");
        var ovuong3 = document.getElementById("ovuong3");
        var ovuong4 = document.getElementById("ovuong4");
        var ovuongcenter = document.getElementById("ovuongcenter");
        ovuong1.style.opacity = 0;
        ovuong2.style.opacity = 0;
        ovuong3.style.opacity = 0;
        ovuong4.style.opacity = 0;
        ovuongcenter.style.backgroundColor = "transparent";
        ovuongcenter.style.border = "none";
        ovuongcenter.style.boxShadow = "0px 0px 0px 0px";

        var theloaicauhoi = "";
        theloaicauhoi += "<p> ĐÃ HẾT CÂU HỎI </p>";
        $('#theloaicauhoi').html(theloaicauhoi);

        var questionHtml = "";
        questionHtml += "<p style=\"text-align: center; margin: 20px; font-family: 'Open Sans', sans-serif; color: white; font-weight: bold; font-size: 15px\">";
        questionHtml += "Bạn có 10 giây để lựa chọn bấm trả lời chướng ngại vật";
        questionHtml += "</p>";
        $('#question-containers').html(questionHtml);

        var donghocat = document.getElementById("donghocat");
        // Thêm class "column" vào phần tử
        donghocat.classList.add("column");

        const timer = setTimeout(() => {
            $('#kiemtrasau10s').trigger('click');
            donghocat.classList.remove("column");
            // Xóa đồng hồ đếm thời gian
            clearTimeout(timer);
        }, 10000);
    }
});

connection.on("NguoiBamCNV", function (list) {
    var listcnv = JSON.parse(list);
    console.log("test bấm cnv");
    console.log(listcnv);
    console.log(listcnv.length);
    var divElement = document.getElementById("hienthinguoibamcnv");
    console.log(divElement);
    var bottompx = 0;
    for (var i = 0; i < listcnv.length; i++)
    {
        console.log("Đây là vòng lặp");
        console.log(i);
        //xóa phần tử ban đầu
        var divxoa = document.getElementById("cnv" + listcnv[i].NguoiDungId.toString());
        if (divxoa)
        {
            divElement.removeChild(divxoa);
        }
        // Tạo một phần tử div mới
        var div = document.createElement("div");
        div.id = "cnv" + listcnv[i].NguoiDungId.toString();
        div.className = "box";
        div.style.width = "250px";
        div.style.marginLeft = "1235px";
        div.style.position = "absolute";
        div.style.bottom = bottompx.toString() + "px";
        div.style.marginBottom = "1px";
        div.style.border = "solid 2px white";

        if (listcnv[i].TrangThaiNguoiChoi == "loai")
        {
            div.style.backgroundColor = "red";
        }

        bottompx += 58;

        var hovaten = listcnv[i].HoVaTen;
        var answer = listcnv[i].Answer;
        // Thêm nội dung của phần tử div mới
        div.innerHTML = `
            <p style="text-align:center; margin-right:35px; font-size: 12px">${hovaten.toUpperCase()}</p>
            <span style="text-align:center ;font-size: 12px">${answer.toUpperCase()}</span>
            `;
        divElement.appendChild(div);
    }
    console.log(listcnv.length);
});

connection.on("ChonNguoiTraLoiCNV", function (nguoidungid) {

    console.log("Đây là thông báo nguoidungid");

    console.log("Người dùng: " + nguoidungid);

    $('#chonnguoitraloi').trigger('click', nguoidungid);

});

connection.on("HienThiCauTraLoiCNV", function (playerid, answer) {
    console.log("Đây là hiển thị trả lời cnv");
    console.log("playerid: " + playerid);
    console.log("answer: " + answer);
    var idplayer = playerid.toString();
    var answerSpan = document.querySelector("#cnv" + idplayer + " span");
    answerSpan.innerHTML = answer.toUpperCase();
});

connection.on("ResutlTraLoiCNV", function (playerid, dapan, checkcnv) {
    console.log("Đây là result cnv");
    console.log("playerid: " + playerid);
    console.log("answer: " + dapan);
    var idplayer = playerid.toString();
    //var dapandungsai = dapan;
    //console.log("dapandungsai: " + dapandungsai);
    //var answerSpan = document.querySelector("#cnv" + idplayer + " span");
    //answerSpan.innerHTML = dapandungsai.toUpperCase();
    var divElement = document.getElementById("cnv" + idplayer);
    if (checkcnv == "dung")
    {
        divElement.style.backgroundColor = "green";
    }
    else if (checkcnv == "sai")
    {
        divElement.style.backgroundColor = "red";
    }
});

let canCall2 = true;
connection.on("GoiCheckLanCuoi", function (loai) {
    if (canCall2) {
        canCall2 = false;
        var loaigoi = parseInt(loai);
        $('#goihamchecklancuoi').trigger('click', loaigoi);
        setTimeout(function () {
            canCall2 = true;
        }, 10000); // Thời gian giữa hai lần gọi hàm là 1000ms
    }
    
});


connection.on("DapAnCNV", function (dapancnv) {
    var divElement = document.getElementById("dapancnv");
    divElement.style.backgroundColor = "deeppink";
    divElement.innerHTML = "Đáp án chướng ngại vật: " + dapancnv.toUpperCase();
});

connection.on("ShowResutl", function (dapancnv) {
    const choncauhoiDivs = document.querySelectorAll('.choncauhoi');
    choncauhoiDivs.forEach(div => {
        const children = div.querySelectorAll('*');
        children.forEach(child => {
            child.style.backgroundColor = "purple";
            child.style.color = 'white';
            child.style.borderColor = 'white';
            child.style.opactity = 0.5;
        });
    });

    var ovuong1 = document.getElementById("ovuong1");
    var ovuong2 = document.getElementById("ovuong2");
    var ovuong3 = document.getElementById("ovuong3");
    var ovuong4 = document.getElementById("ovuong4");
    var ovuongcenter = document.getElementById("ovuongcenter");
    ovuong1.style.opacity = 0;
    ovuong2.style.opacity = 0;
    ovuong3.style.opacity = 0;
    ovuong4.style.opacity = 0;
    ovuongcenter.style.backgroundColor = "transparent";
    ovuongcenter.style.border = "none";
    ovuongcenter.style.boxShadow = "0px 0px 0px 0px";

    var divElement = document.getElementById("dapancnv");
    divElement.style.backgroundColor = "deeppink";
    divElement.innerHTML = "Đáp án chướng ngại vật: " + dapancnv.toUpperCase();
});

//vòng 3
var goi3 = 0;
var goi4 = 0;

var _canCall3 = true;
var _lastCalledTime3 = Date.now() - 10000; // set to 10 seconds ago

connection.on("UpdateAnswer3", function (html1) {

    var currentTime = Date.now();
    if (_canCall3 && (currentTime - _lastCalledTime3) >= 10000) {
        goi3++;
        console.log("Đây là update answer");
        console.log("Số lần update: " + goi3);
        if (goi3 == 4 || goi3 == 8 || goi3 == 12 || goi3 == 16 || goi3 == 20) {
            $('#playerNscore3').empty();
            $('#playerNscore3').html(html1);
        }

        // End of your code
        setTimeout(function () {
            _canCall3 = true;
        }, 19500);
    }
});


var _canCall4 = true;
var _lastCalledTime4 = Date.now() - 10000; // set to 10 seconds ago
connection.on("UpdateResult3", function (html) {

    var currentTime = Date.now();
    if (_canCall4 && (currentTime - _lastCalledTime4) >= 10000) {
        goi4++;
        console.log("Đây là update result");
        console.log("Số lần update: " + goi4);
        if (goi4 == 4 || goi4 == 8 || goi4 == 12 || goi4 == 16 || goi4 == 20) {
            $('#playerNscore3').empty();
            $('#playerNscore3').html(html);           
        }

        // End of your code
        setTimeout(function () {
            _canCall = true;
        }, 19500);
    }
});

connection.on("UpdateScore3", function (html) {
    $('#playerNscore3').empty();
    $('#playerNscore3').html(html);


});


var _canCallv3 = true;
let canCallv3 = true;
var _lastCalledTimev3 = Date.now() - 10000; // set to 10 seconds ago
connection.on("NextQuestionV3", function (slg) {
    if (canCallv3)
    {
        canCallv3 = false;
        if (slg < 4)
        {
            $('#hienthicauhoitieptheo').trigger('click', slg);
        }
        else
        {
            //var url = '@Url.Action("Index", "PhongCho", new { scheme = Request.Scheme })';
            //window.location.assign(url);
        }
        setTimeout(function () {
            canCallv3 = true;
        }, 10000);

    }
    
});

//vòng 4
connection.on("UpdateScore4", function (html) {
    $('#playerNscore4').empty();
    $('#playerNscore4').html(html);
});

connection.on("CapNhatGoiCau1", function (goicauhoi) {
    document.getElementById("diemcau1").innerHTML = "CÂU 1 - " + goicauhoi.toString() + " ĐIỂM";
});
connection.on("CapNhatGoiCau2", function (goicauhoi) {
    document.getElementById("diemcau2").innerHTML = "CÂU 2 - " + goicauhoi.toString() + " ĐIỂM";
});
connection.on("CapNhatGoiCau3", function (goicauhoi) {
    document.getElementById("diemcau3").innerHTML = "CÂU 3 - " + goicauhoi.toString() + " ĐIỂM";

    //gọi hàm hỏi chọn ngôi sao hy vọng
    $('#hoichonngoisao').trigger('click');
});


var _canCallv4 = true;
let canCallv4 = true;
var _lastCalledTimev4 = Date.now() - 10000; // set to 10 seconds ago
connection.on("UpdateQuestionVong4", function (cauhoiv4, cauhoiso, checknshv) {
    if (canCallv4) {
        canCallv4 = false;
        var question = JSON.parse(cauhoiv4);

        var questionHtml = "";
        questionHtml += "<p style=\"text-align: center; margin: 20px; font-family: 'Open Sans', sans-serif; color: white; font-weight: bold; font-size: 15px\">";
        questionHtml += question.NoiDung;
        questionHtml += "</p>";
        $('#question-containers').html(questionHtml);

        var donghocat = document.getElementById("donghocat");
        // Thêm class "column" vào phần tử
        donghocat.classList.add("column4");

        console.log(donghocat);

        //màu xanhhh câu hỏi
        if (cauhoiso != 1)
        {
            var thediv = "divdiemcau" + (cauhoiso - 1).toString();
            var div = document.getElementById(thediv);
            div.style.backgroundColor = "none";
        }
        var thediv = "divdiemcau" + cauhoiso.toString();
        var div = document.getElementById(thediv);
        div.style.backgroundColor = "green";

        //gọi hàm lấy đáp án
        var idcauhoi = parseInt(question.CauHoiId);
        

        setTimeout(function () {
            canCallv4 = true;
            donghocat.classList.remove("column4");
            $('#choosecheckanswer').trigger('click', [idcauhoi, checknshv]);
        }, 15000);

    }
});

connection.on("UpdateAnswer4", function (traloi) {
    console.log("Đây là updateanswer4");
    var ht = document.getElementById("hienthicautraloi");
    ht.innerHTML = traloi.toUpperCase();
    ht.style.backgroundColor = "transparent";
});

connection.on("UpdateResult4", function (cauhoiid, result, solangoicauhoi, solangoi) {
    var pp = document.getElementById("hienthicautraloi");
    if (result == 1)
    {
        pp.style.backgroundColor = "green";

        //next
    }
    else
    {
        pp.style.backgroundColor = "red";

        setTimeout(function () {
            $('#moinguoikhactraloi').trigger('click', [cauhoiid, solangoicauhoi, solangoi]);
        }, 3000);
    }

});
connection.on("ThongBaoDapAnV4", function (dapan) {
    var pp = document.getElementById("hienthidapancauhoi");
    pp.innerHTML = "Đáp án: " + dapan;
});

connection.on("NguoiBamTTL", function (hovaten) {
    var divElement = document.getElementById("hienthianh");
        // Tạo một phần tử div mới
    var div = document.createElement("div");
    div.id = "divttl";
    div.className = "box";
    div.style.width = "350px";
    div.style.marginLeft = "1135px";
    div.style.position = "absolute";
    div.style.bottom = "0px";
    div.style.marginBottom = "1px";
    div.style.border = "solid 2px white";
    var answer = "";

        // Thêm nội dung của phần tử div mới
    div.innerHTML = `
            <p style="text-align:center; margin-right:35px; font-size: 12px">${hovaten.toUpperCase()}</p>
            <span style="text-align:center ;font-size: 12px">${answer.toUpperCase()}</span>
            `;
    divElement.appendChild(div);

    //vô hiệu hóa btn
    var btn = document.getElementById("btnTTL");
    if (btn != null)
    {
        btn.remove();
    }
});
var cnttl = 0;
connection.on("ChonNguoiTTL", function (idplayer, idcauhoi, solangoicauhoi, solangoi) {
    cnttl++;
    console.log("fix bug cnttl");
    console.log("cnttl: " + cnttl);
    if (cnttl == 3) {
        cnttl = 0;
        console.log("đây là connection.on ChonNguoiTTL");
        console.log("cnttl: " + cnttl);
        console.log("idplayer: " + idplayer);
        console.log("idcauhoi: " + idcauhoi);
        console.log("solangoicauhoi: " + solangoicauhoi);
        console.log("solangoi: " + solangoi);
        $('#chonnguoittl').trigger('click', [idplayer, idcauhoi, solangoicauhoi, solangoi]);
    }
});

connection.on("UpdateResultTTL", function (hovaten, cautraloi, dapan, result) {
    var divElement = document.getElementById("divttl");
    divElement.innerHTML = `
            <p style="text-align:center; margin-right:35px; font-size: 12px">${hovaten.toUpperCase()}</p>
            <span style="text-align:center ;font-size: 12px">${cautraloi.toUpperCase()}</span>
            `;
    if (result == 1) {
        divElement.style.backgroundColor = "green";
    }
    else
    {
        divElement.style.backgroundColor = "red";
    }
    var div = document.getElementById("hienthidapancauhoi");
    div.innerHTML = "Đáp án: " + dapan.toUpperCase();
});

connection.on("NextQuestion4", function (solangoicauhoi, solangoi)
{
    if (solangoicauhoi == 3 && solangoi == 4) {
        setTimeout(function () {
            var url = '@Url.Action("Index", "PhongCho", new { scheme = Request.Scheme })';
            window.location.assign(url);
        }, 5000);
    }
    else
    {
        $('#nextquestion').trigger('click', [solangoicauhoi, solangoi]);
    }
});

var goikhongaitraloi = 0;
connection.on("GoiKhongAiTraLoi", function (phongdauid, cauhoiid, solangoicauhoi, solangoi) {
    goikhongaitraloi++;
    if (goikhongaitraloi == 3)
    {
        goikhongaitraloi = 0;
        $('#khongaitraloi').trigger('click', [phongdauid, cauhoiid, solangoicauhoi, solangoi]);
    }
});

connection.on("CheckBtnTLL", function (cauhoiid, solangoicauhoi, solangoi) {
    $('#checkbtnTTL').trigger('click', [cauhoiid, solangoicauhoi, solangoi]);
});

connection.start().then(function () {
    //dosomething
}).catch(function (err) {
    console.error(err.toString());
});