"use strict"

/* api */

const GET_LANGUAGES = "/api/languages";

/* Get languages */

function getLanguages(result) {
	$.ajax(GET_LANGUAGES, {
    success: result,
    error: function(error) {
      console.log('error ' + error);
    }
	});
}