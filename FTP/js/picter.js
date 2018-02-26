$(function () {
    var _this = $(this);
    var _index = _this.index();

    now = _index;
    func();
    $(".fullpop").show();
    $(".foodpop").css("display", "block");
    $(".foodpop").animate({
        marginTop: "-300px",
        opacity: '1'
    }, 500);
})
$(function () {
    $(".fullpop").hide();
    $(".foodpop").animate({
        marginTop: "-1284px",
        opacity: '1'
    }, 500, function () {
        $(".foodpop").css("display", "none");
    })
})
$(function () {
    now--;
    if (now < 0) {
        now = 0;
    }
    func();
})
$(function () {
    now++;
    if (now > total) {
        now = total;
    }
    func();
})


