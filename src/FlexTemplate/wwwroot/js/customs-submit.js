
jQuery(function($) {

	"use strict";
		
	/**
	 * WYSIHTML5 -  A better approach to rich text editing
	 */
	$('.bootstrap3-wysihtml5').wysihtml5({});
	
	
	/**
	 * Tokenfield for Bootstrap
	 * http://sliptree.github.io/bootstrap-tokenfield/
	 */
	 
	$('.tokenfield').tokenfield()

	// Autocomplete Tagging
	var engine = new Bloodhound({
		local: [{value: 'Italian'}, {value: 'Seafood'}, {value: 'Bistro'} , {value: 'Western'}, {value: 'French'}, {value: 'Thai'}, {value: 'Japanese'}, {value: 'Sushi'}, {value: 'Arabic'}],
		datumTokenizer: function(d) {
			return Bloodhound.tokenizers.whitespace(d.value);
		},
		queryTokenizer: Bloodhound.tokenizers.whitespace
	});
	engine.initialize();
	$('#autocompleteTagging').tokenfield({
		typeahead: [null, { source: engine.ttAdapter() }]
	});
	
	//  Dropzone -----------------------------------------------------------------------------------------------------------

	if( $('.dropzone').length > 0 ) {
			Dropzone.autoDiscover = false;
			$("#file-submit").dropzone({
					url: "upload",
					addRemoveLinks: true
			});

			$("#profile-picture").dropzone({
					url: "upload",
					addRemoveLinks: true
			});
			
			$(".food-menu-image").dropzone({
					url: "upload",
					maxFiles: 1,
					addRemoveLinks: true
			});
	}

	//  Timepicker ---------------------------------------------------------------------------------------------------------

	if( $('.oh-timepicker').length > 0 ) {
			$('.oh-timepicker').timepicker();
	}
	
	
	//  Map Submit location -----------------------------------------------------------------------------------------------------------
	
	var mapStyles = [ {"featureType":"road","elementType":"labels","stylers":[{"visibility":"simplified"},{"lightness":20}]},{"featureType":"administrative.land_parcel","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"landscape.man_made","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"transit","elementType":"all","stylers":[{"saturation":-100},{"visibility":"on"},{"lightness":10}]},{"featureType":"road.local","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"road.local","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"road.highway","elementType":"labels","stylers":[{"visibility":"simplified"}]},{"featureType":"poi","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"road.arterial","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":50}]},{"featureType":"water","elementType":"all","stylers":[{"hue":"#a1cdfc"},{"saturation":30},{"lightness":49}]},{"featureType":"road.highway","elementType":"geometry","stylers":[{"hue":"#f49935"}]},{"featureType":"road.arterial","elementType":"geometry","stylers":[{"hue":"#fad959"}]}, {featureType:'road.highway',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-92},{lightness:60},{visibility:'on'}]}, {featureType:'landscape.natural',elementType:'all',stylers:[{hue:'#c8c6c3'},{saturation:-71},{lightness:-18},{visibility:'on'}]},  {featureType:'poi',elementType:'all',stylers:[{hue:'#d9d5cd'},{saturation:-70},{lightness:20},{visibility:'on'}]} ];

	function simpleMap(_latitude, _longitude, draggableMarker){
			var mapCenter = new google.maps.LatLng(_latitude, _longitude);
			var mapOptions = {
					zoom: 14,
					center: mapCenter,
					disableDefaultUI: true,
					scrollwheel: false,
					styles: mapStyles,
					panControl: false,
					zoomControl: false,
					draggable: true
			};
			var mapElement = document.getElementById('map-simple');
			var map = new google.maps.Map(mapElement, mapOptions);

			// Google map marker content -----------------------------------------------------------------------------------

			var markerContent = document.createElement('DIV');
			markerContent.innerHTML =
					'<div class="map-marker">' +
							'<div class="icon"></div>' +
					'</div>';

			// Create marker on the map ------------------------------------------------------------------------------------

			var marker = new RichMarker({
					//position: mapCenter,
					position: new google.maps.LatLng( _latitude, _longitude ),
					map: map,
					draggable: draggableMarker,
					content: markerContent,
					flat: true
			});

			marker.content.className = 'marker-loaded';
	}
	
	
	$(window).load(function(){
		var _latitude = 51.541599;
		var _longitude = -0.112588;
		var draggableMarker = true;
		simpleMap(_latitude, _longitude,draggableMarker);
	});
	
});

