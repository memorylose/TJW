$(function () {
    InputText();
    TableInput();
    SetTableRowColor();
    if (request('editId') != null) {
        FileImgCtl();
        EditCustomFlag();
        EditPicHrefFlag();
        EditPicWordFlag();
    }
});

//main page menu click
function ClickAdminMenu(menuName) {
    $(menuName).slideToggle();
}

//set all admin text focus and blur
function InputText() {
    $('.input_text').focus(function () {
        $(this).css('border', '2px solid #63AEE8');
    });
    $('.input_text').blur(function () {
        $(this).css('border', '1px solid #DDDDDD');
    });

}

//set table input focus
function TableInput() {
    $('.td_input').focus(function () {
        $('.td_imageWBtn').css('display', 'block');
        $('.td_imageRBtn').css('display', 'block');
        $('.td_input').select();
    });
    $('.td_input').blur(function () {
    });

}
//set table input cancel button
function TableInputCancel() {
    $('.td_imageWBtn').css('display', 'none');
    $('.td_imageRBtn').css('display', 'none');
}

//Check delete
function delcfm(msg) {
    if (!confirm(msg)) {
        return false;
    }
}

//Display two cloth type button
function DBtn(className) {
    $(className).css('display', 'block');
}

//Set color for table every row
function SetTableRowColor() {
    $(".adminTable tr:even").addClass("tableColor");
}

//Show picture
function UploadChange(value, files) {
    var objUrl = getObjectURL(files);
    console.log("objUrl = " + objUrl);
    if (objUrl) {
        $("#fileImg").attr("src", "123");
        FileImgCtl();
    }
}
function getObjectURL(file) {
    var url = null;
    if (window.createObjectURL != undefined) { // basic
        url = window.createObjectURL(file);
    } else if (window.URL != undefined) { // mozilla(firefox)
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) { // webkit or chrome
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}
function FileImgCtl() {
    $("#fileImgDiv").css("height", "70px");
    $(".MiddleRight img").css("display", "block");
    $(".btnUp").css("margin-top", "25px");
    $(".btnUp").css("margin-left", "15px");
}

function request(strParame) {
    var args = new Object();
    var query = location.search.substring(1);
    var pairs = query.split("&");
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        value = decodeURIComponent(value);
        args[argname] = value;
    }
    return args[strParame];
}
function CustomCheck() {
    if ($("#ckCustomBH").is(":checked")) {
        $('#cusDp').css("display", 'block');
    }
    else {
        $('#cusDp').css("display", 'none');
    }
}
function CustomAddressCheck() {
    if ($("#ckCustomAd").is(":checked")) {
        $('#cusDp').css("display", 'block');
    }
    else {
        $('#cusDp').css("display", 'none');
    }
}
function CustomWordCheck() {
    if ($("#ckCustomWord").is(":checked")) {
        $('#cusWord').css("display", 'block');
    }
    else {
        $('#cusWord').css("display", 'none');
    }
}
function EditCustomFlag() {
    if ($('#hdCustomFlag').val() == '1') {
        $('#ckCustomBH').prop('checked', 'checked');
    }
}
function EditPicHrefFlag() {
    if ($('#hdCustomFlag').val() == '1') {
        $('#ckCustomAd').prop('checked', 'checked');
    }
}
function EditPicWordFlag() {
    if ($('#hdCustomWordFlag').val() == '1') {
        $('#ckCustomWord').prop('checked', 'checked');
    }
}