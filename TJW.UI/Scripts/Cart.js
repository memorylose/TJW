$(function () {
    request('1');
    SetFlag();
    AddCart();
    AddTeaCart();

});

function Buy() {
    var result = true;
    var traget = document.getElementById('sizeDiv');
    if (traget.style.display != "none") {
        if ($('#hidColor').val() == '') {
            alert('请选择颜色');
            result = false;
        }
        else if ($('#hidSize').val() == '') {
            alert('请选择大小');
            result = false;
        }
    }
    return result;
}

function AddCart() {

    $('#cartId').click(function (e) {
        var flag = '0';
        if ($('#hidCusFlag').val() == '0') {
            if ($('#hidColor').val() == '') {
                alert('请选择颜色');
            }
            else if ($('#hidSize').val() == '') {
                alert('请选择大小');
            }
            else {
                if ($('#countId').text() == '已售罄' || $('#countId').text() == '<span style=\'color:red\'>已售罄</span>') {
                    alert('该商品无货');
                }
                else {
                    flag = '1';
                }

            }
        }
        else {
            flag = '1';
        }

        if (flag == '1') {
            $.ajax({
                url: '/AjaxHandler/UIAjaxHandler.ashx?clothGuid=' + request('clothGUID') + '&colorName=' + encodeURI($('#hidColor').val()) + '&sizeName=' + $('#hidSize').val() + '&dpCusId=' + $('#dpCustom').val(),
                type: "post",
                data: { Method: 'GetClothUGUID' },
                success: function (msg) {
                    if (msg == '0') {
                        alert('添加购物车失败');
                    }
                    else {
                        $.ajax({
                            url: '/AjaxHandler/UIAjaxHandler.ashx?clothId=' + msg + '&clothCount=' + $('#carNum').val() + '&dpCusId=' + $('#dpCustom').val(),
                            type: "post",
                            data: { Method: 'AddCart' },
                            success: function (msg) {
                                if (msg == '-1') {
                                    e.preventDefault();
                                    jError('购物车中已经存在此类商品', {
                                        TimeShown: 1000
                                    });
                                }
                                else if (msg == '-5') {
                                    e.preventDefault();
                                    jError('您的购物车中已有超过10件商品，请先提交订单或删除一些商品。', {
                                        TimeShown: 1000
                                    });
                                }
                                else if (msg == '-3') {
                                    e.preventDefault();
                                    jError('添加购物车失败', {
                                        TimeShown: 1000
                                    });
                                }
                                else if (msg == '-2') {
                                    location.href = '/Login.aspx?cart=' + request('clothGUID');
                                }
                                else if (msg == '1') {
                                    e.preventDefault();
                                    jSuccess('添加购物车成功', {
                                        TimeShown: 1000
                                    });
                                }
                            }
                        });
                    }
                }
            });
        }
    });
}
function AddTeaCart() {
    $('#teaCartId').click(function (e) {
        $.ajax({
            url: '/AjaxHandler/UIAjaxHandler.ashx?teaGUID=' + request('teaGUID') + '&teaCount=' + $('#carNum').val(),
            type: "post",
            data: { Method: 'AddTeaCart' },
            success: function (msg) {
                if (msg == '-1') {
                    e.preventDefault();
                    jError('购物车中已经存在此类商品', {
                        TimeShown: 1000
                    });
                }
                else if (msg == '-5') {
                    e.preventDefault();
                    jError('您的购物车中已有超过10件商品，请先提交订单或删除一些商品。', {
                        TimeShown: 1000
                    });
                }
                else if (msg == '-2') {
                    location.href = '/Login.aspx?teaCart=' + request('teaGUID');
                }
                else if (msg == '-3') {
                    e.preventDefault();
                    jError('添加购物车失败', {
                        TimeShown: 1000
                    });
                }
                else if (msg == '1') {
                    e.preventDefault();
                    jSuccess('添加购物车成功', {
                        TimeShown: 1000
                    });
                }
            }
        });
    });
}
function ColorClick(divId) {
    var j = $('.detail_color').size();
    var otherDiv = divId.substring(0, 7);
    for (var i = 1; i < j + 1; i++) {

        $('#' + otherDiv + i).css('border', '1px solid #DDDDDD');
    }
    $('#' + divId).css('border', '2px solid #D62476');
    $('#hidColor').prop('value', $('#' + divId).text());
    $.ajax({
        url: '../AjaxHandler/UIAjaxHandler.ashx?clothGuid=' + request('clothGUID') + '&colorName=' + encodeURI($('#' + divId).text()),
        type: "post",
        data: { Method: 'GetCartSize' },
        success: function (msg) {
            if (msg != '' && msg != null) {
                $('#sizeList').html(msg);
            }

        }
    });
}
function SizeClick(divId) {
    var j = $('.detail_size').size();
    var otherDiv = divId.substring(0, 6);
    for (var i = 1; i < j + 1; i++) {
        $('#' + otherDiv + i).css('border', '1px solid #DDDDDD');
    }
    $('#' + divId).css('border', '2px solid #D62476');
    $('#hidSize').prop('value', $('#' + divId).text());

    if ($('#hidColor').val() != '' && $('#hidSize').val() != '') {

        $.ajax({
            url: '../AjaxHandler/UIAjaxHandler.ashx?colorName=' + encodeURI($('#hidColor').val()) + '&sizeName=' + $('#hidSize').val() + '&guid=' + request(''),
            type: "post",
            data: { Method: 'GetStoreCount_Detail' },

            success: function (msg) {
                if (msg == '-1') {
                    //参数错误
                }
                else {
                    $('#countId').text(msg);
                    if ($('#countId').text() == '0') {
                        $('#countId').text('已售罄');
                        $('#countId').css('color', 'red');
                    }
                    else {
                        $('#countId').css('color', 'black');
                    }
                }

            }
        });
    }
}
function request(strParame) {
    //var args = new Object();
    //var query = location.search.substring(1);
    //var pairs = query.split("&");
    //for (var i = 0; i < pairs.length; i++) {
    //    var pos = pairs[i].indexOf('=');
    //    if (pos == -1) continue;
    //    var argname = pairs[i].substring(0, pos);
    //    var value = pairs[i].substring(pos + 1);
    //    value = decodeURIComponent(value);
    //    args[argname] = value;
    //}
    //return args[strParame];
    var query = window.location.href;
    var pos = query.lastIndexOf('/');
    return query.substring(pos + 1);
}
function CartDown() {
    var number = parseInt($('#carNum').val());
    if (number > 1) {
        $('#carNum').prop('value', number - 1);
    }
}
function CartUp() {
    var number = parseInt($('#carNum').val());
    if (number < 10) {
        $('#carNum').prop('value', number + 1);
    }
}
function SetFlag() {
    $('#hidColor').prop('value', '');
    $('#hidSize').prop('value', '');
}
function CheckCartDelete(clothName, cartId) {
    $.layer({
        shade: [0],
        area: ['auto', 'auto'],
        dialog: {
            msg: '您正要删除<span style="color:red;">' + clothName + '</span>，确认吗？',
            btns: 2,
            type: 4,
            btn: ['确认', '取消'],
            yes: function () {
                location.href = 'MyCart.aspx?CartId=' + cartId;
            }, no: function () {

            }
        }
    });
}
//checkbox all
function AllSelected() {
    //if ($(this).attr("checked") == 'true') {
    //    alert(1);
    //}
    //else {
    //    //$("input[type=checkbox]").each(function () {
    //    //    $(this).attr("checked", false);
    //    //});
    //    alert(0);
    //}

    if ($('#ckAll').is(":checked")) {

        $(":checkbox").each(function (i) {

            if ($(this).attr("checked")) {

                alert($(this).val());

            }

        });
    }
    else {

        $("input[type=checkbox]").each(function () {

        });
    }

}
function AddCartNum(id, type) {
    id = id.substring(4);

    var num = 1;
    if (type == '0') {
        if ($('#input_' + id).val() > 1) {
            num = -1;
            $('#input_' + id).prop('value', parseInt($('#input_' + id).val()) + num);
        }
    }

    if (type == '1') {
        if ($('#input_' + id).val() < 10) {
            num = 1;
            $('#input_' + id).prop('value', parseInt($('#input_' + id).val()) + num);
        }
    }
}

function SetHiddenCart() {
    var count = '';
    $("input[type=text]").each(function (a, b) {
        var id = $(b).attr("id");
        if (id.indexOf('input_') != '-1') {
            count += $('#' + id).val() + ',';
        }
    });
    count = count.substring(0, count.length - 1);
    $('#hdCount').prop('value', count);
}