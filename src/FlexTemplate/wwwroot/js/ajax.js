"use strict"

/* Get languages */

function getLanguages() {
	let languages;
	$.ajax({
	  url: "/api/languages",
	  context: document.body,
	  success: function(){
	    languages = this;
	  }
	});
	return languages; 
}   
    