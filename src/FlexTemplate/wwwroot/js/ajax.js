"use strict"

/* api */

const GET_LANGUAGES = "http://localhost:5000/api/languages";

/* Get languages */

function getLanguages(result) {
	$.ajax(GET_LANGUAGES, {
    success: result,
    error: function(error) {
      console.log('error ' + error);
    }
	});
}   
    