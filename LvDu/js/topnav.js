$(function(){
	$(".navbox li").mouseout(function(){
		$(this).find(".childernav").css("display", "none");
	})
	$(".navbox li").mouseover(function(){
		$(this).find(".childernav").css("display", "block");
	})
})