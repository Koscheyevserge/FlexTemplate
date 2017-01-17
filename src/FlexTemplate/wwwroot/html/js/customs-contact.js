
function initialize() {

	var styles = [{"featureType":"all","elementType":"labels","stylers":[{"lightness":63},{"hue":"#ff0000"}]},{"featureType":"administrative","elementType":"all","stylers":[{"hue":"#000bff"},{"visibility":"on"}]},{"featureType":"administrative","elementType":"geometry","stylers":[{"visibility":"on"}]},{"featureType":"administrative","elementType":"labels","stylers":[{"color":"#4a4a4a"},{"visibility":"on"}]},{"featureType":"administrative","elementType":"labels.text","stylers":[{"weight":"0.01"},{"color":"#727272"},{"visibility":"on"}]},{"featureType":"administrative.country","elementType":"labels","stylers":[{"color":"#ff0000"}]},{"featureType":"administrative.country","elementType":"labels.text","stylers":[{"color":"#ff0000"}]},{"featureType":"administrative.province","elementType":"geometry.fill","stylers":[{"visibility":"on"}]},{"featureType":"administrative.province","elementType":"labels.text","stylers":[{"color":"#545454"}]},{"featureType":"administrative.locality","elementType":"labels.text","stylers":[{"visibility":"on"},{"color":"#737373"}]},{"featureType":"administrative.neighborhood","elementType":"labels.text","stylers":[{"color":"#7c7c7c"},{"weight":"0.01"}]},{"featureType":"administrative.land_parcel","elementType":"labels.text","stylers":[{"color":"#404040"}]},{"featureType":"landscape","elementType":"all","stylers":[{"lightness":16},{"hue":"#ff001a"},{"saturation":-61}]},{"featureType":"poi","elementType":"labels.text","stylers":[{"color":"#828282"},{"weight":"0.01"}]},{"featureType":"poi.government","elementType":"labels.text","stylers":[{"color":"#4c4c4c"}]},{"featureType":"poi.park","elementType":"all","stylers":[{"hue":"#00ff91"}]},{"featureType":"poi.park","elementType":"labels.text","stylers":[{"color":"#7b7b7b"}]},{"featureType":"road","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"road","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"labels.text","stylers":[{"color":"#999999"},{"visibility":"on"},{"weight":"0.01"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"hue":"#ff0011"},{"lightness":53}]},{"featureType":"road.highway","elementType":"labels.text","stylers":[{"color":"#626262"}]},{"featureType":"transit","elementType":"labels.text","stylers":[{"color":"#676767"},{"weight":"0.01"}]},{"featureType":"water","elementType":"all","stylers":[{"hue":"#0055ff"}]}];
	
	var styledMap = new google.maps.StyledMapType(styles,  {name: "Styled Map"});
	
	var secheltLoc = new google.maps.LatLng(25.20, 55.2718),
		 markers,
				myMapOptions = {
				 zoom: 14,
				center: secheltLoc,
				mapTypeId: google.maps.MapTypeId.ROADMAP,
				panControl: false,
				rotateControl: false,
				streetViewControl: false,
				scrollwheel: false,
		},
		map = new google.maps.Map(document.getElementById("single_office_map"), myMapOptions);

	//Associate the styled map with the MapTypeId and set it to display.
	map.mapTypes.set('map_style', styledMap);
	map.setMapTypeId('map_style');

	function initMarkers(map, markerData) {
			var newMarkers = [],
					marker;
					
			for(var i=0; i<markerData.length; i++) {
					marker = new google.maps.Marker({
							map: map,
							draggable: true,
							position: markerData[i].latLng,
							visible: true,
							icon: 'images/map-marker/00.png',
					}),
					boxText = document.createElement("div"),
					//these are the options for all infoboxes
					infoboxOptions = {
					content: boxText,
							disableAutoPan: false,
							maxWidth: 0,
							pixelOffset: new google.maps.Size(0, -50),
							zIndex: null,
							closeBoxMargin: '0',
							closeBoxURL: "images/infobox-close.png",
							infoBoxClearance: new google.maps.Size(1, 1),
							isHidden: false,
							pane: "floatPane",
							enableEventPropagation: false,
							alignBottom: true,
					};
					
					newMarkers.push(marker);
					boxText.innerHTML =  "<h6 class='infoBox-contact-branch mb-5'>" + markerData[i].contactBranch + "</h6>";
					//Define the infobox
					newMarkers[i].infobox = new InfoBox(infoboxOptions);
					//Open box when page is loaded
					newMarkers[i].infobox.open(map, marker);
					//Add event listen, so infobox for marker is opened when user clicks on it.  Notice the return inside the anonymous function - this creates
					//a closure, thereby saving the state of the loop variable i for the new marker.  If we did not return the value from the inner function, 
					//the variable i in the anonymous function would always refer to the last i used, i.e., the last infobox. This pattern (or something that
					//serves the same purpose) is often needed when setting function callbacks inside a for-loop.
					google.maps.event.addListener(marker, 'click', (function(marker, i) {
							return function() {
									newMarkers[i].infobox.open(map, this);
									map.panTo(markerData[i].latLng);
							}
					})(marker, i));
			}
			
			return newMarkers;
	}
	
	//here the call to initMarkers() is made with the necessary data for each marker.  All markers are then returned as an array into the markers variable
	markers = initMarkers(map, [
			{ latLng: new google.maps.LatLng(25.20, 55.2718), contactBranch: "We Are Here" },
	]);
}
