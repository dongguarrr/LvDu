$(function() {

	// 页面浮动面板
	$("#floatPanel > .ctrolPanel > a.arrow").eq(0).click(function() {
		$("html,body").animate({
			scrollTop: 0
		}, 800);
		return false;
	});
	$("#floatPanel > .ctrolPanel > a.arrow").eq(1).click(function() {
		$("html,body").animate({
			scrollTop: $(document).height()
		}, 800);
		return false;
	});

	var objPopPanel = $("#floatPanel > .popPanel");
	var w = objPopPanel.outerWidth();
	
	$("#floatPanel > .ctrolPanel > a.qrcode").bind({
		mouseover: function() {
			objPopPanel.css("width", "0px").show();
			objPopPanel.animate({
				"width": 240 + "px"
			}, 300);
			console.log(w);
			return false;
		},
		mouseout: function() {
			objPopPanel.animate({
				"width": "0px"
			}, 300);
			return false;
			objPopPanel.css("width", w + "px");
		}
	});
});