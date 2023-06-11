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

//file vong2



connection.on("UpdateAnswer", function (html1) {
    //goi1++;
    //console.log("Đây là update answer");
    //console.log("Số lần update: " + goi1);
    //if (goi1 == 4 || goi1 == 8 || goi1 == 12 || goi1 == 16 || goi1 == 20) {
    //    $('#playerNscore2').empty();
    //    $('#playerNscore2').html(html1);
    //}

    var currentTime = Date.now();
    if (_canCall && (currentTime - _lastCalledTime) >= 10000) {
        goi1++;
        console.log("Đây là update answer");
        console.log("Số lần update: " + goi1);
        if (goi1 == 4 || goi1 == 8 || goi1 == 12 || goi1 == 16 || goi1 == 20) {
            $('#playerNscore2').empty();
            $('#playerNscore2').html(html1);
        }

        // End of your code
        setTimeout(function () {
            _canCall = true;
        }, 19500);
    }
});


connection.on("UpdateResult", function (html, idcauhoi, result) {
    //goi2++;
    //console.log("Đây là update result");
    //console.log("Số lần update: " + goi2);
    //if (goi2 == 4 || goi2 == 8 || goi2 == 12 || goi2 == 16 || goi1 == 20) {
    //    $('#playerNscore2').empty();
    //    $('#playerNscore2').html(html);
    //    console.log("Thông báo result signalR " + result);
    //    $('#hienthidapan').trigger('click', [idcauhoi, result]);
    //}

    var currentTime = Date.now();
    if (_canCall && (currentTime - _lastCalledTime) >= 10000) {
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
    /*goi++;*/
    //console.log("Đây là update 2");
    //console.log("Số lần update: " + goi);
    //console.log(listnc);
    /*var listnctld = JSON.parse(listnc);*/
    $('#playerNscore2').empty();
    $('#playerNscore2').html(html);


});

//connection.on("UpdateScore2", function (html, listnc) {
//    /*goi++;*/
//    console.log("Đây là update score2");
//    //console.log("Số lần update: " + goi);
//    //console.log(listnc);
//    var listnctld = JSON.parse(listnc);
//    $('#playerNscore2').empty();
//    $('#playerNscore2').html(html);
    
    
//});

var updategoi = 0;
connection.on("UpdateQuestion", function (cauhoi2, hangngang) {
    updategoi++;
    console.log("số lần gọi thời gian:" + updategoi);
    var cauhoiv2 = JSON.parse(cauhoi2);
    var hangngangchon = hangngang - 1;
    var question = cauhoiv2[hangngangchon];

    var idcauhoi = question.CauHoiId;
    $('#hienthiovuong').trigger('click', idcauhoi);

    var theloaicauhoi = "";
    theloaicauhoi += "<p> CÂU " + "1" + " - " + question.LinhVuc.TenLinhVuc.toUpperCase();
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
        var idcauhoi = parseInt(question.CauHoiId);
        donghocat.classList.remove("column");
        $('#submitButton2').trigger('click', [idchon, idcauhoi]);
    }, 10000);


    //var currentTime = Date.now();
    //if (_canCall && (currentTime - _lastCalledTime) >= 10000) {
    //    setTimeout(function () {
    //    var idchon = 1;
    //    var idcauhoi = parseInt(question.CauHoiId);
    //    donghocat.classList.remove("column");
    //    $('#submitButton2').trigger('click', [idchon, idcauhoi]);
    //}, 10000);

    //    // End of your code
    //    setTimeout(function () {
    //        _canCall = true;
    //    }, 19500);
    //}

});

//connection.on("NextQuestionne", function (solangoi) {

//    console.log("Đây là thông báo log");

//    console.log("Số lần gọi: " + solangoi);

//    $('#hienthinguoitieptheo').trigger('click', solangoi);

//    //if (solangoi == 4 || solangoi > 4) {
//    //    $('#hienthinguoitieptheo').trigger('click', solangoi - 2);
//    //}
//    //else {
//    //    $('#hienthinguoitieptheo').trigger('click', solangoi);
//    //}

//});

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
        else
        {
            //var randomIndex = Math.floor(Math.random() * cauhoichuahoi.length);
            //var randomElement = cauhoichuahoi[randomIndex];
            /*var idchon = 0;*/
            /*var idhangngang = randomElement + 1;*/
            var idhangngang = cauhoichuahoi[0] + 1;
            /*console.log("random hàng ngang số: " + idhangngang)*/
            var currentTime = Date.now();
            if (_canCall && (currentTime - _lastCalledTime) >= 10000)
            {
                setTimeout(function () {
                    donghocat.classList.remove("column");
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

        const timer = setTimeout(() => {
            $('#kiemtrasau10s').trigger('click');
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

connection.on("GoiCheckLanCuoi", function (loai) {
    var loaigoi = parseInt(loai);
    $('#goihamchecklancuoi').trigger('click', loaigoi);
});

connection.start().then(function () {
    //dosomething
}).catch(function (err) {
    console.error(err.toString());
});