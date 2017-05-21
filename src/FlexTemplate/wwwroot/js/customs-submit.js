
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
	const URL_DOMAIN = window.location.origin;
	$('.tokenfield').tokenfield();

    // Autocomplete Tagging
	if ($('#autocompleteTagging').length > 0) {
	    $.ajax({
	        datatype: 'json',
	        url: URL_DOMAIN + "/api/resources/tags",
	        data: "",
	        success: function (data) {
	            if (data) {
	                var engine = new Bloodhound({
	                    local: data.map(function(item) {
	                        return { value: item };
	                    }),
	                    datumTokenizer: function (d) {
	                        return Bloodhound.tokenizers.whitespace(d.value);
	                    },
	                    queryTokenizer: Bloodhound.tokenizers.whitespace
	                });
	                engine.initialize();
	                $('#autocompleteTagging').tokenfield({
	                    typeahead: [null, { source: engine.ttAdapter() }]
	                });
	            }
	        }
	    });
    }
	//  Dropzone -----------------------------------------------------------------------------------------------------------
	function urlExists(url, callback) {
	    var http = new XMLHttpRequest();
	    http.open('HEAD', url);
        http.onload = function(e) {
            if (this.status != 404) {
                callback.call();
            }
        }
	    http.send();
	}
	if ($('.dropzone').length > 0) {
			Dropzone.autoDiscover = false;

			$("#profile-picture").dropzone({
					url: "upload",
					addRemoveLinks: true
			});

            let arrayFoodMenuImage = $(".food-menu-image");

            for (let i = 0; i < arrayFoodMenuImage.length; i++) {
                let self = $(arrayFoodMenuImage[i]);
                self.dropzone({
                    url: "/api/upload/producthead/" + self.find(".file_descriptor").val(),
                    addRemoveLinks: true,
                    maxFiles: 1,
                    init: function() {
                        if (self.hasClass("food-menu-image-exsisting")) {
                            var mockFile = { name: self.find(".file_descriptor").val() + ".jpg", size: 0, type: 'image/jpeg' };
                            this.addFile.call(this, mockFile);
                            this.options.thumbnail.call(this, mockFile, "../../Resources/Products/" + self.find(".file_descriptor").val() + ".jpg");
                        }
                    },
                    removedfile: function(file) {
                        $.ajax({
                            type: 'DELETE',
                            url: window.location.origin + '/api/upload/producthead/' + self.find(".file_descriptor").val(),
                            data: "",
                            dataType: 'text'
                        });
                        var _ref;
                        return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                    }
                });
            }
			$("#edit-place").dropzone({
			    url: "/api/upload/newplace/" + $("#edit-place").find(".file_descriptor").val(),
			    addRemoveLinks: true,
			    removedfile: function(file) {
			        $.ajax({
			            type: 'DELETE',
			            url: window.location.origin + '/api/upload/newplace/' + $("#edit-place").find(".file_descriptor").val() + "/" + $(file.previewElement).attr("realFilename"),
			            data: "",
			            dataType: 'text'
			        });
			        var _ref;
			        return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
			    },
                success: function(file){
                    $(file.previewElement).attr("realFilename", file.xhr.response);
			    },
			    init: function () {
			        var dropzone = this;
			        var url = window.location.href.split("/").pop();
			        if (!Number.isNaN(url) && window.location.href.indexOf('lace') > -1) {
			            $.get(window.location.origin + "/api/resources/photo-detail/" + url, function (data) {
			                for (let i = 0; i < data.length; i++) {
			                    var file = data[i].replace("wwwroot", "..\\..\\");
			                    file = file.split("/").pop();
			                    var mockFile = { name: file, size: 0, type: 'image/jpeg' };
			                    dropzone.addFile.call(dropzone, mockFile);
			                    dropzone.options.thumbnail.call(dropzone, mockFile, file);
			                }
			            });
			        }
			    }
			});
			$("#new-place").dropzone({
			    url: "/api/upload/newplace/" + $("#new-place").find(".file_descriptor").val(),
			    addRemoveLinks: true,
			    success: function(file) {
			        $(file.previewElement).attr("realFilename", file.xhr.response);
			    },
			    removedfile: function (file) {
			        $.ajax({
			            type: 'DELETE',
			            url: window.location.origin + '/api/upload/newplace/' + $("#new-place").find(".file_descriptor").val() + "/" + $(file.previewElement).attr("realFilename"),
			            data: "",
			            dataType: 'text'
			        });
			        var _ref;
			        return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
			    }
			});
	        $("#place-head-update").dropzone({
	            url: "/api/upload/placehead/" + $("#place-head-update").find(".file_descriptor").val(),
	            addRemoveLinks: true,
	            maxFiles:1,
	            init: function () {
	                var dropzone = this;
	                var url = window.location.href.split("/").pop();
	                if (!Number.isNaN(url) && window.location.href.indexOf('lace') > -1) {
	                    var file = "..\\..\\Resources\\Places\\" + url + "\\head.jpg";
	                    urlExists(file, function() {
	                            //file = file.split("/").pop();
	                            var mockFile = { name: file, size: 0, type: 'image/jpeg' };
	                            this.addFile.call(this, mockFile);
	                            this.options.thumbnail.call(this, mockFile, file);
	                        }.bind(dropzone));
	                }
	            },
	            removedfile: function (file) {
	                $.ajax({
	                    type: 'DELETE',
	                    url: window.location.origin + '/api/upload/placehead/' + $("#place-head-update").find(".file_descriptor").val(),
	                    data: "",
	                    dataType: 'text'
	                });
	                var _ref;
	                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
	            }
	        });
	        $("#blog-head-update").dropzone({
	            url: "/api/upload/bloghead/" + $("#blog-head-update").find(".file_descriptor").val(),
	            addRemoveLinks: true,
	            maxFiles: 1,
	            init: function () {
	            },
	            removedfile: function (file) {
	                $.ajax({
	                    type: 'DELETE',
	                    url: window.location.origin + '/api/upload/bloghead/' + $("#blog-head-update").find(".file_descriptor").val(),
	                    data: "",
	                    dataType: 'text'
	                });
	                var _ref;
	                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
	            }
	        });
	        $("#place-banner-update").dropzone({
	            url: "/api/upload/placebanner/" + $("#place-banner-update").find(".file_descriptor").val(),
	            addRemoveLinks: true,
	            maxFiles: 1,
	            init: function () {
	                var dropzone = this;
	                var url = window.location.href.split("/").pop();
	                if (!Number.isNaN(url) && window.location.href.indexOf('lace') > -1) {
	                    var file = "..\\..\\Resources\\Places\\" + url + "\\banner.jpg";
	                    urlExists(file, function () {
                            //file = file.split("/").pop();
                            var mockFile = { name: file, size: 0, type: 'image/jpeg' };
                            this.addFile.call(this, mockFile);
                            this.options.thumbnail.call(this, mockFile, file);
                        }.bind(dropzone));
	                }
	            },
	            removedfile: function (file) {
	                $.ajax({
	                    type: 'DELETE',
	                    url: window.location.origin + '/api/upload/placebanner/' + $("#place-banner-update").find(".file_descriptor").val(),
	                    data: "",
	                    dataType: 'text'
	                });
	                var _ref;
	                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
	            }
	        });
	        $("#blog-banner-update").dropzone({
	            url: "/api/upload/blogbanner/" + $("#blog-banner-update").find(".file_descriptor").val(),
	            addRemoveLinks: true,
	            maxFiles: 1,
	            init: function () {
	            },
	            removedfile: function (file) {
	                $.ajax({
	                    type: 'DELETE',
	                    url: window.location.origin + '/api/upload/blogbanner/' + $("#blog-banner-update").find(".file_descriptor").val(),
	                    data: "",
	                    dataType: 'text'
	                });
	                var _ref;
	                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
	            }
	        });
	        $("#file-submit").dropzone({
	            url: "/api/upload/newplace/" + window.location.href.split("/").pop()
	        });
	}

	//  Timepicker ---------------------------------------------------------------------------------------------------------

	if( $('.oh-timepicker').length > 0 ) {
			$('.oh-timepicker').timepicker();
	}
	
	
	//  Map Submit location -----------------------------------------------------------------------------------------------------------
	
	var mapStyles = [ {"featureType":"road","elementType":"labels","stylers":[{"visibility":"simplified"},{"lightness":20}]},{"featureType":"administrative.land_parcel","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"landscape.man_made","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"transit","elementType":"all","stylers":[{"saturation":-100},{"visibility":"on"},{"lightness":10}]},{"featureType":"road.local","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"road.local","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"road.highway","elementType":"labels","stylers":[{"visibility":"simplified"}]},{"featureType":"poi","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"road.arterial","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":50}]},{"featureType":"water","elementType":"all","stylers":[{"hue":"#a1cdfc"},{"saturation":30},{"lightness":49}]},{"featureType":"road.highway","elementType":"geometry","stylers":[{"hue":"#f49935"}]},{"featureType":"road.arterial","elementType":"geometry","stylers":[{"hue":"#fad959"}]}, {featureType:'road.highway',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-92},{lightness:60},{visibility:'on'}]}, {featureType:'landscape.natural',elementType:'all',stylers:[{hue:'#c8c6c3'},{saturation:-71},{lightness:-18},{visibility:'on'}]},  {featureType:'poi',elementType:'all',stylers:[{hue:'#d9d5cd'},{saturation:-70},{lightness:20},{visibility:'on'}]} ];
    if ($("#lat-hidden").val() && $("#lng-hidden").val()) {
            $("#map-simple").ready(function () {
                var _latitude = $("#lat-hidden").val();
                var _longitude = $("#lng-hidden").val();
                var mapCenter = new google.maps.LatLng(_latitude, _longitude);
                var mapOptions = {
                    zoom: 14,
                    center: mapCenter,
                    disableDefaultUI: true,
                    scrollwheel: true,
                    styles: mapStyles,
                    panControl: true,
                    zoomControl: true,
                    draggable: true
                };
                var mapElement = document.getElementById('map-simple');
                if (!mapElement)
                    return;
                // Google map marker content -----------------------------------------------------------------------------------
                var map = new google.maps.Map(mapElement, mapOptions);
                var markerContent = document.createElement('DIV');
                markerContent.innerHTML =
                    '<div class="map-marker">' +
                    '<div class="icon"></div>' +
                    '</div>';

                // Create marker on the map ------------------------------------------------------------------------------------

                var marker = new RichMarker({
                    //position: mapCenter,
                    position: new google.maps.LatLng(_latitude, _longitude),
                    map: map,
                    draggable: true,
                    content: markerContent,
                    flat: true
                });
                google.maps.event.addListener(marker, 'dragend', function () {
                    $("#lat-hidden").val(this.position.lat());
                    $("#lng-hidden").val(this.position.lng());
                });
                marker.content.className = 'marker-loaded';
            });
    } else if (navigator.geolocation) {
        if (false) {
	        navigator.geolocation.getCurrentPosition(function(position) {
	            $("#map-simple").ready(function () {
                    var _latitude = position.coords.latitude;
                    var _longitude = position.coords.longitude;
                    var mapCenter = new google.maps.LatLng(_latitude, _longitude);
                    var mapOptions = {
                        zoom: 14,
                        center: mapCenter,
                        disableDefaultUI: true,
                        scrollwheel: true,
                        styles: mapStyles,
                        panControl: true,
                        zoomControl: true,
                        draggable: true
                    };
                    var mapElement = document.getElementById('map-simple');
                    if (!mapElement)
                        return;
                    // Google map marker content -----------------------------------------------------------------------------------
                    var map = new google.maps.Map(mapElement, mapOptions);
                    var markerContent = document.createElement('DIV');
                    markerContent.innerHTML =
                        '<div class="map-marker">' +
                        '<div class="icon"></div>' +
                        '</div>';

                    // Create marker on the map ------------------------------------------------------------------------------------

                    var marker = new RichMarker({
                        //position: mapCenter,
                        position: new google.maps.LatLng(_latitude, _longitude),
                        map: map,
                        draggable: true,
                        content: markerContent,
                        flat: true
                    });
                    $("#lat-hidden").val(_latitude);
                    $("#lng-hidden").val(_longitude);
                    google.maps.event.addListener(marker, 'dragend', function () {
                        $("#lat-hidden").val(this.position.lat());
                        $("#lng-hidden").val(this.position.lng());
                    });
                    marker.content.className = 'marker-loaded';
	            });
	        });
        } else {
            $("#map-simple").ready(function () {
                var _latitude = "50.5";
                var _longitude = "30.5";
                var mapCenter = new google.maps.LatLng(_latitude, _longitude);
                var mapOptions = {
                    zoom: 14,
                    center: mapCenter,
                    disableDefaultUI: true,
                    scrollwheel: true,
                    styles: mapStyles,
                    panControl: true,
                    zoomControl: true,
                    draggable: true
                };
                var mapElement = document.getElementById('map-simple');
                if (!mapElement)
                    return;
                // Google map marker content -----------------------------------------------------------------------------------
                var map = new google.maps.Map(mapElement, mapOptions);
                var markerContent = document.createElement('DIV');
                markerContent.innerHTML =
                    '<div class="map-marker">' +
                    '<div class="icon"></div>' +
                    '</div>';

                // Create marker on the map ------------------------------------------------------------------------------------

                var marker = new RichMarker({
                    //position: mapCenter,
                    position: new google.maps.LatLng(_latitude, _longitude),
                    map: map,
                    draggable: true,
                    content: markerContent,
                    flat: true
                });
                $("#lat-hidden").val(_latitude);
                $("#lng-hidden").val(_longitude);
                google.maps.event.addListener(marker, 'dragend', function () {
                    $("#lat-hidden").val(this.position.lat());
                    $("#lng-hidden").val(this.position.lng());
                });
                marker.content.className = 'marker-loaded';
            });
        }
    } else {
        alert("Sorry, your browser does not support HTML5 geolocation.");
    }
});