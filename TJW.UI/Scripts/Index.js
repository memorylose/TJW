// JavaScript source code
$(function () {
    IndexPictureChange();
});

//��ҳͼƬ�ֻ�
function IndexPictureChange() {
    $("#slider4").responsiveSlides({
        maxwidth: 620,
        auto: true,
        pager: false,
        nav: true,
        speed: 500,
        namespace: "callbacks",
        before: function () {
            $('.events').append("<li>before event fired.</li>");
        },
        after: function () {
            $('.events').append("<li>after event fired.</li>");
        }
    });
}