//动画效果
jQuery.easing['jswing'] = jQuery.easing['swing'];
jQuery.extend(jQuery.easing, {
	easeOutBounce: function(x, t, b, c, d) {
		if((t /= d) < (1 / 2.75)) {
			return c * (7.5625 * t * t) + b
		} else if(t < (2 / 2.75)) {
			return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b
		} else if(t < (2.5 / 2.75)) {
			return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b
		} else {
			return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b
		}
	},
	easeOutSine: function(x, t, b, c, d) {
		return c * Math.sin(t / d * (Math.PI / 2)) + b
	},
	easeInOutSine: function(x, t, b, c, d) {
		return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b
	},
	easeOutExpo: function(x, t, b, c, d) {
		return(t == d) ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b
	},
	easeInOutBack: function(x, t, b, c, d, s) {
		if(s == undefined) s = 1.70158;
		if((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
		return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b
	}
});


$(document).ready(function() {
var now=0;
    var total = $(".container-fluid #box").size()-1;

    $(".container-fluid #box").click(function(){
	var _this=$(this);
	var _index=_this.index();

	now=_index;
	func();
	$(".fullpop").show();
	$(".foodpop").css("display","block");
	$(".foodpop").animate({marginTop:"-300px",opacity:'1'},500);
})

function func(){
	$(".leftBar").show();
	$(".rightBar").show();
	if(now==0){$(".leftBar").hide();}
	if(now==total){$(".rightBar").hide();}
    var getImg = $(".container-fluid #box").eq(now).find("img").attr("data-img");
    var getInfo = $(".container-fluid #box").eq(now).find("p").eq(1).html();
    var getName = $(".container-fluid #box").eq(now).find("p").eq(0).html();
	$("#popimg").attr("src",getImg);
	$("#foodName").html(getName);
	$("#foodInfo").html(getInfo);
}



$(".rightBar").click(function(){
	now++;
	if(now>total){now=total;}
	func();
})
$(".leftBar").click(function(){
	now--;
	if(now<0){now=0;}
	func();
})


$(".foodpop_clo").click(function(){
	$(".fullpop").hide();
	$(".foodpop").animate({marginTop:"-1284px",opacity:'1'},500,function(){$(".foodpop").css("display","none");})
})
})