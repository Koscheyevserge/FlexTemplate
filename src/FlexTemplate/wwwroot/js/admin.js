"use strict"

const POST_UPDATE_CATEGORY = "/api/category/update";
const POST_CREATE_CATEGORY = "/api/category/create"; 
const GET_REMOVE_CATEGORY = "/api/category/delete/";

const POST_UPDATE_ALIAS = "/api/categoryalias/update"; 
const POST_CREATE_ALIAS = "/api/category/createalias/";
const GET_REMOVE_ALIAS = "/api/categoryalias/delete/";

/* url */
const URL_DOMAIN = window.location.origin;

/* create alias */
$('.c-categories').on('click', '.admin-button-add-alias', function() {
	let child = this;
	$.ajax({
	    url: POST_CREATE_ALIAS + $(this).parents('.category').attr('dataId'),
	    datatype: 'json',
	    type: "get", 
	    success: function (data) {
	        $(child).parent().before(data);
	    }
	});
});


/* remove alias */
$('.c-categories').on('click','.admin-button-alias-remove', function() {
	let alias = $(this).parent();;
	$.ajax({
	  dataType: "json",
	  url: URL_DOMAIN + GET_REMOVE_ALIAS + alias.attr('dataId'),
	  data: "",
    success: function(data) {
    	if (data.successed) {
				alias.remove();
			}	else {
    		alert('Error: ' + data.errorMessages);
			}
    	console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
    }
	});
});


/* update alias */
$('.c-categories').on('click', '.admin-button-alias-save', function() {
	let alias = $(this).parent();
  $.ajax({
    url: URL_DOMAIN + POST_UPDATE_ALIAS,
    datatype: 'json',
    type: "post", 
    contentType: "application/json",
    data: JSON.stringify({
			id: +alias.attr('dataId'),
			text: alias.find('input').val(),
			languageId: +alias.find('select :selected').attr('dataId'),
			categoryId: +alias.parents('.category').attr('dataId')
		}),
  	success: function(data) {
    	if (!data.successed) {
    		alert('Error: ' + data.errorMessages);
    	}
  		console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
  	}
  });
});


/* create category */
$('.c-categories').on('click', '.admin-button-add-category', function() {
  $.ajax({
    url: POST_CREATE_CATEGORY,
    datatype: 'json',
    type: "get",
    success:  function (data) {
        $('.c-categories .categories').append(data);
    }
  });
});


/* remove category */
$('.c-categories').on('click', '.admin-button-remove-category', function() {
	let parent = $(this).parent();
	$.ajax({
	  dataType: "json",
	  url: URL_DOMAIN + GET_REMOVE_CATEGORY + parent.attr('dataId'),
	  data: "",
    success: function(data) {
    	if (data.successed) {
				parent.remove();
    	} else {
    		alert('Error: ' + data.errorMessages);
    	}
    	console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
    }
	});
});		    			 		

/* update category */
$('.c-categories').on('click', '.admin-button-category-save', function() {
	let parent = $(this).parents(".category");
	$.ajax({
	  url: URL_DOMAIN + POST_UPDATE_CATEGORY,
    datatype: 'json',
    type: "post", 
    contentType: "application/json",
	  data: JSON.stringify({
			id: +parent.attr("dataId"),
			Name: parent.find("#categoryName").val()
	  }),
		success: function(data) {
    	if (!data.successed) {
    		alert('Error: ' + data.errorMessages);
    	}
			console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
		}
	});
});