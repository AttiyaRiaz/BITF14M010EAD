jQuery(document).ready(function($) {
	$('.imageRotator > button').on('click', function() {
		$('.rotator').toggleClass('rotate-3d');
	}); 
 
	/*fancybox
	 ==============================*/
	$("#gallery li a, .fancybox").fancybox();
	
	/* hover Gallery
	 ==============================*/
	$('#gallery .item').hover(function() {
		$(this).find('.hover').toggleClass('animated bounceIn');
	});
	//=================================== Tooltips =====================================//
	if ($.fn.tooltip()) {
		$('.list-brands [class="tooltip_hover"]').tooltip({
			placement : "top"
		});

		$('.top ul.social li a, ul.social-foot li a').tooltip({
			placement : "bottom"
		});				
		
	}


	//=================================== animations =================================//

	var lefts = $(".from-left"), rights = $(".from-right"), bottoms = $(".from-bottom"), rotates = $(".rotate"), left_tops = lefts.map(function() {
		return $(this).offset().top;
	}), right_tops = rights.map(function() {
		return $(this).offset().top;
	}), bottom_tops = bottoms.map(function() {
		return $(this).offset().top;
	}), rotate_tops = rotates.map(function() {
		return $(this).offset().top;
	});

	$(window).load(function() {
		left_tops = lefts.map(function() {
			return $(this).offset().top;
		});
		right_tops = rights.map(function() {
			return $(this).offset().top;
		});
		bottom_tops = bottoms.map(function() {
			return $(this).offset().top;
		});
		rotate_tops = rotates.map(function() {
			return $(this).offset().top;
		});
	});

	$(window).scroll(function() {
		/*------------------------------------------------------------
		 All Template Animation
		 -------------------------------------------------------------*/
		var scrollVisible = $(window).scrollTop() + $(window).height();
		$(left_tops).each(function(i) {
			if (scrollVisible > this)
				$(lefts[i]).css({
					left : 0
				});
		});

		$(right_tops).each(function(i) {
			if (scrollVisible > this)
				$(rights[i]).css({
					right : 0
				});
		});

		$(bottom_tops).each(function(i) {
			if (scrollVisible > this - 1000)
				$(bottoms[i]).css({
					bottom : 0
				});
		});

		$(rotate_tops).each(function(i) {
			if (scrollVisible > this + 3000)
				$(rotates[i]).addClass("rotate-normal");
		});
		/*------------------------------------------------------------
		 End All Template Animation
		 -------------------------------------------------------------*/
	});
	//=================================== form forms =================================//
	$("#newsletter-form, #contact, #suscribe-form, .price").submit(function() {
		var elem = $(this);
		var urlTarget = $(this).attr("action");
		$.ajax({
			type : "POST",
			url : urlTarget,
			dataType : "html",
			data : $(this).serialize(),
			beforeSend : function() {
				elem.prepend("<div class='alert alert-info'>" + "<a class='close' data-dismiss='alert'>×</a>" + "Loading" + "</div>");
				//elem.find(".loading").show();
			},
			success : function(response) {
				elem.prepend(response);
				elem.find(".response").html(response);
				elem.find(".alert-info").hide();
				//elem.find(".alert-danger").hide();
				elem.find("input[type='text'],input[type='email'],textarea").val("");
			}
		});
		return false;
	});

	//=================================== animated anchors =================================//
	
	$('a.anchor ').click(function() {
		
		if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {

			var $target = $(this.hash);

			$target = $target.length && $target || $('[name=' + this.hash.slice(1) + ']');

			if ($target.length) {

				var targetOffset = $target.offset().top;

				$('html,body').animate({
					scrollTop : targetOffset
				}, 1000);

				return false;

			}

		}

	});
	
	

	

	// services
	// ==============================


	
	var owl = $("#owl-demo");

	owl.owlCarousel({
		items : 4, //10 items above 1000px browser width
		itemsDesktop : [1000, 4], //5 items between 1000px and 901px
		itemsDesktopSmall : [900, 2], // betweem 900px and 601px
		itemsTablet : [600, 1], //2 items between 600 and 0
		autoPlay : 3000,
		navigation: true,
		navigationText	: ["<i class='icon-left-open'></i>","<i class='icon-right-open'></i>"],
		itemsMobile : false // itemsMobile disabled - inherit from itemsTablet option
	});

	

	var owl2 = $("#owl-services");

	owl2.owlCarousel({
		items : 4, //10 items above 1000px browser width
		itemsDesktop : [1000, 3], //5 items between 1000px and 901px
		itemsDesktopSmall : [900, 2], // betweem 900px and 601px
		itemsTablet : [600, 1], //2 items between 600 and 0
		autoPlay : 3000,
		navigation: true,
		navigationText	: ["<i class='icon-left-open'></i>","<i class='icon-right-open'></i>"],
		itemsMobile : false // itemsMobile disabled - inherit from itemsTablet option
	});

	
	
	// Slider Rotator Script

	//=================================== video responsivo  =================================//

	$(".video").fitVids();

	//=================================== steps animation =================================//
	$('#features .item').hover(function() {
		$(this).children(".icon").children("figure").children("img").addClass('animated flip');
	}, function() {
		$(this).children(".icon").children("figure").children("img").removeClass('animated flip');
	});


	//=================================== fix menu on scroll =================================//
	
	//alert (ScrollTop);
	$(window).scroll(function() {

		var headerBottom = 0;

		var ScrollTop = $(window).scrollTop();
		
		//alert (ScrollTop);
		//console.log(ScrollTop);
		
		if (ScrollTop > headerBottom) {
			$("header").addClass("fixed");
		}
		if (ScrollTop == headerBottom) {
			$("header").removeClass("fixed");
		}
	});

});
