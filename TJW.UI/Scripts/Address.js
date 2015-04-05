$(function () {
    BindProvince();
});

//bind province
function BindProvince() {
    $("#slProvince").get(0).options.add(new Option("请选择", "请选择"));
    $("#slCity").get(0).options.add(new Option("请选择", "请选择"));
    $("#slDistinct").get(0).options.add(new Option("请选择", "请选择"));
    $.ajax({
        url: '../AjaxHandler/UIAjaxHandler.ashx',
        dataType: 'json',
        data: { Method: 'BindProvince' },
        success: function (msg) {
            $(msg).each(function (i) {
                var OpText = msg[i].ProvinceName;
                var OpValue = msg[i].ProvinceId;
                $("#slProvince").get(0).options.add(new Option(OpText, OpValue));
            });
        }
    });
}

//bindcity
function ChangeCity(value) {
    if (value == "请选择") {
        $("#slCity").get(0).options.length = 0;
        $("#slCity").get(0).options.add(new Option("请选择", "请选择"));

        $("#slDistinct").get(0).options.length = 0;
        $("#slDistinct").get(0).options.add(new Option("请选择", "请选择"));
    }
    else {
        $("#slCity").get(0).options.length = 0;
        $.ajax({
            url: '../AjaxHandler/UIAjaxHandler.ashx?proId=' + value,
            dataType: 'json',
            data: { Method: 'BindCity' },
            success: function (msg) {
                $(msg).each(function (i) {
                    var OpText = msg[i].CityName;
                    var OpValue = msg[i].CityId;
                    $("#slCity").get(0).options.add(new Option(OpText, OpValue));
                });
            }
        });
        ChangeDistinct(value);
    }
}
//bind distinct
function ChangeDistinct(value) {
    if (value == "请选择") {
        $("#slDistinct").get(0).options.length = 0;
        $("#slDistinct").get(0).options.add(new Option("请选择", "请选择"));
    }
    else {
        $("#slDistinct").get(0).options.length = 0;
        $.ajax({
            url: '../AjaxHandler/UIAjaxHandler.ashx?cityId=' + value,
            dataType: 'json',
            data: { Method: 'BindDistinct' },
            success: function (msg) {
                $(msg).each(function (i) {
                    var OpText = msg[i].DistrictName;
                    var OpValue = msg[i].DistrictId;
                    $("#slDistinct").get(0).options.add(new Option(OpText, OpValue));
                });
            }
        });
    }
}
//cancel
function CancelBtn() {
    $('.add_address_div').css('display', 'none');
}

//submit
function SubmitBtn() {
    var result = true;
    if ($("#slProvince").val() == '请选择') {
        $('#msgProvinceId').css('color', '#FA817E');
        result = false;
    }
    else if ($('#txtJD').val().length < 5 || $('#txtJD').val().length > 100) {
        $('#msgProvinceId').css('color', '#999999');

        $('#txtJD').css('border', '1px solid #FA817E');
        $('#msgDistinctId').css('color', '#FA817E');
        result = false;
    }
    else if (isCode($('#txtCode').val()) == false) {
        $('#txtJD').css('border', '1px solid #DDDDDD');
        $('#msgDistinctId').css('color', '#999999');

        $('#txtCode').css('border', '1px solid #FA817E');
        $('#msgCodeId').css('color', '#FA817E');
        result = false;
    }
    else if (isDigit($('#txtTel').val()) == false) {
        $('#txtCode').css('border', '1px solid #DDDDDD');
        $('#msgCodeId').css('color', '#999999');

        $('#txtTel').css('border', '1px solid #FA817E');
        $('#msgTelId').css('color', '#FA817E');
        result = false;
    }
    else {
        $('#txtTel').css('border', '1px solid #DDDDDD');
        $('#msgTelId').css('color', '#999999');
    }
    return result;

}
//check phone number
function isDigit(s) {
    var patrn = /^\d{11}$/;
    if (!patrn.exec(s)) {
        return false;
    }
    else {
        return true;
    }
}
//check code
function isCode(s) {
    var patrn = /^[1-9][0-9]{5}$/
    if (!patrn.exec(s)) {
        return false;
    }
    else {
        return true;
    }
}
//show address
function ShowAddress() {
    $.ajax({
        url: '../AjaxHandler/UIAjaxHandler.ashx?userName=' + $('#userAId').text() + '',
        type: "post",
        data: { Method: 'GetAddressCount' },
        success: function (msg) {
            if (msg == '3') {
                jError('对不起，您最多只能添加三个地址', {
                    TimeShown: 1000
                });
            }
            else {
                $('.add_address_div').css('display', 'block');
            }
        }
    });

}
//delete check
function CheckCartDelete(delId) {
    $.layer({
        shade: [0],
        area: ['auto', 'auto'],
        dialog: {
            msg: '您确认要删除此地址吗？',
            btns: 2,
            type: 4,
            btn: ['确认', '取消'],
            yes: function () {
                location.href = 'MyAddress.aspx?delId=' + delId;
            }, no: function () {

            }
        }
    });
}
