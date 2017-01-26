
jQuery(function($) {

	"use strict";
		
	/**
	 * Image Grid for Photo
	 */	
		
	$('#detail-food-photo').imagesGrid({
			images: [
					{ src: 'images/detail-food-photo/01.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption One' },
					{ src: 'images/detail-food-photo/02.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Two' },
					{ src: 'images/detail-food-photo/03.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Three' },
					{ src: 'images/detail-food-photo/04.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Fpur' },
					{ src: 'images/detail-food-photo/05.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Five' },
					{ src: 'images/detail-food-photo/06.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Six' },
					{ src: 'images/detail-food-photo/07.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Seven' },
					{ src: 'images/detail-food-photo/08.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Eight' },
					{ src: 'images/detail-food-photo/09.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Nine' },
					{ src: 'images/detail-food-photo/10.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Ten' },
					{ src: 'images/detail-food-photo/11.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Eleven' },
					{ src: 'images/detail-food-photo/12.jpg', alt: 'Second image', title: 'Second image', caption: 'Image Caption Twelve' },
			],
			cells: 5,
			align: true
	});
	
});


/**
 * Google Map for Detail Location
 */	
function initialize() {

	// Create an array of styles.
	var styles = [{"featureType":"all","elementType":"labels","stylers":[{"lightness":63},{"hue":"#ff0000"}]},{"featureType":"administrative","elementType":"all","stylers":[{"hue":"#000bff"},{"visibility":"on"}]},{"featureType":"administrative","elementType":"geometry","stylers":[{"visibility":"on"}]},{"featureType":"administrative","elementType":"labels","stylers":[{"color":"#4a4a4a"},{"visibility":"on"}]},{"featureType":"administrative","elementType":"labels.text","stylers":[{"weight":"0.01"},{"color":"#727272"},{"visibility":"on"}]},{"featureType":"administrative.country","elementType":"labels","stylers":[{"color":"#ff0000"}]},{"featureType":"administrative.country","elementType":"labels.text","stylers":[{"color":"#ff0000"}]},{"featureType":"administrative.province","elementType":"geometry.fill","stylers":[{"visibility":"on"}]},{"featureType":"administrative.province","elementType":"labels.text","stylers":[{"color":"#545454"}]},{"featureType":"administrative.locality","elementType":"labels.text","stylers":[{"visibility":"on"},{"color":"#737373"}]},{"featureType":"administrative.neighborhood","elementType":"labels.text","stylers":[{"color":"#7c7c7c"},{"weight":"0.01"}]},{"featureType":"administrative.land_parcel","elementType":"labels.text","stylers":[{"color":"#404040"}]},{"featureType":"landscape","elementType":"all","stylers":[{"lightness":16},{"hue":"#ff001a"},{"saturation":-61}]},{"featureType":"poi","elementType":"labels.text","stylers":[{"color":"#828282"},{"weight":"0.01"}]},{"featureType":"poi.government","elementType":"labels.text","stylers":[{"color":"#4c4c4c"}]},{"featureType":"poi.park","elementType":"all","stylers":[{"hue":"#00ff91"}]},{"featureType":"poi.park","elementType":"labels.text","stylers":[{"color":"#7b7b7b"}]},{"featureType":"road","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"road","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"labels.text","stylers":[{"color":"#999999"},{"visibility":"on"},{"weight":"0.01"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"hue":"#ff0011"},{"lightness":53}]},{"featureType":"road.highway","elementType":"labels.text","stylers":[{"color":"#626262"}]},{"featureType":"transit","elementType":"labels.text","stylers":[{"color":"#676767"},{"weight":"0.01"}]},{"featureType":"water","elementType":"all","stylers":[{"hue":"#0055ff"}]}];

	var loc, map, marker, infobox;

	var styledMap = new google.maps.StyledMapType(styles,  {name: "Styled Map"});

	loc = new google.maps.LatLng($("#hotel-detail-map").attr("data-lat"), $("#hotel-detail-map").attr("data-lon"));

	map = new google.maps.Map(document.getElementById("hotel-detail-map"), {
		zoom: 14,
		center: loc,
		scrollwheel: false,
		//draggable:true,
		navigationControl: false,
		scaleControl: false,
		mapTypeControl:false,
		streetViewControl: false,
		mapTypeControlOptions: {
			mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'map_style']
		},
		mapTypeId: google.maps.MapTypeId.ROADMAP,
	});

	//Associate the styled map with the MapTypeId and set it to display.
	map.mapTypes.set('map_style', styledMap);
	map.setMapTypeId('map_style');

	marker = new google.maps.Marker({
		map: map,
		position: loc,
		//disableDefaultUI:true,

		icon:'images/map-marker/00.png',
		//pixelOffset: new google.maps.Size(-140, -100),
		visible: true

		//animation: google.maps.Animation.DROP
	});

	infobox = new InfoBox({
		content: document.getElementById("infobox"),
		disableAutoPan: true,
		//maxWidth: 150,
		pixelOffset: new google.maps.Size(0, -50),
		zIndex: null,
		alignBottom: true,
		isHidden: false,
		//closeBoxMargin: "12px 4px 2px 2px",
		closeBoxURL: "images/infobox-close.png",
		closeBoxClass:"infoBox-close",
		infoBoxClearance: new google.maps.Size(1, 1)
	});

	openInfoBox(marker);

	google.maps.event.addListener(marker, 'click', function() {
		openInfoBox(this);
	});

	function openInfoBox(thisMarker){
		map.panTo(loc);
		map.panBy(0,-80);
		infobox.open(map, thisMarker);
	}

	var center;
	function calculateCenter() {
		center = map.getCenter();
	}
	google.maps.event.addDomListener(map, 'idle', function() {
		calculateCenter();
	});
	google.maps.event.addDomListener(window, 'resize', function() {
		map.setCenter(center);
	});

}
google.maps.event.addDomListener(window, 'load', initialize);



/**
 * Menu Sicky for Detail
 */	
var stickyHeaders = (function() {

	var $window = $(window),
			$stickies;

	var load = function(stickies) {

		if (typeof stickies === "object" && stickies instanceof jQuery && stickies.length > 0) {

			$stickies = stickies.each(function() {

				var $thisSticky = $(this).wrap('<div class="followWrap" />');
	
				$thisSticky
						.data('originalPosition', $thisSticky.offset().top)
						.data('originalHeight', $thisSticky.outerHeight())
							.parent()
							.height($thisSticky.outerHeight()); 			  
			});

			$window.off("scroll.stickies").on("scroll.stickies", function() {
			_whenScrolling();		
			});
		}
	};

	var _whenScrolling = function() {

		$stickies.each(function(i) {			

			var $thisSticky = $(this),
					$stickyPosition = $thisSticky.data('originalPosition');

			if ($stickyPosition <= $window.scrollTop()) {        
				
				var $nextSticky = $stickies.eq(i + 1),
						$nextStickyPosition = $nextSticky.data('originalPosition') - $thisSticky.data('originalHeight');

				$thisSticky.addClass("fixed");

				if ($nextSticky.length > 0 && $thisSticky.offset().top >= $nextStickyPosition) {

					$thisSticky.addClass("absolute").css("top", $nextStickyPosition);
				}

			} else {
				
				var $prevSticky = $stickies.eq(i - 1);

				$thisSticky.removeClass("fixed");

				if ($prevSticky.length > 0 && $window.scrollTop() <= $thisSticky.data('originalPosition') - $thisSticky.data('originalHeight')) {

					$prevSticky.removeClass("absolute").removeAttr("style");
				}
			}
		});
	};

	return {
		load: load
	};
})();

$(function() {
	stickyHeaders.load($(".multiple-sticky"));
});

// Cache selectors
var lastId,
	topMenu = $("#top-menu"),
	topMenuHeight = topMenu.outerHeight()+105,
	// All list items
	menuItems = topMenu.find("a"),
	// Anchors corresponding to menu items
	scrollItems = menuItems.map(function(){
		var item = $($(this).attr("href"));
		if (item.length) { return item; }
	});

// Bind click handler to menu items
// so we can get a fancy scroll animation
menuItems.click(function(e){
	var href = $(this).attr("href"),
			offsetTop = href === "#" ? 0 : $(href).offset().top-125;
			// offsetTop = href === "#" ? 0 : $(href).offset().top-topMenuHeight+1;
	$('html, body').stop().animate({ 
			scrollTop: offsetTop
	}, 300);
	e.preventDefault();
});

// Bind to scroll
$(window).scroll(function(){
	 // Get container scroll position
	 var fromTop = $(this).scrollTop()+topMenuHeight;
	 
	 // Get id of current scroll item
	 var cur = scrollItems.map(function(){
		 if ($(this).offset().top < fromTop)
			 return this;
	 });
	 // Get the id of the current element
	 cur = cur[cur.length-1];
	 var id = cur && cur.length ? cur[0].id : "";
	 
	 if (lastId !== id) {
		 lastId = id;
		 // Set/remove active class
		 menuItems.parent().removeClass("active").end().filter("[href='#"+id+"']").parent().addClass("active");;
	 }                   
});