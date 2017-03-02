"use strict"

const POST_UPDATE_CATEGORY = "/api/category/update";
const POST_CREATE_CATEGORY = "/api/category/create"; 
const GET_REMOVE_CATEGORY = "/api/category/delete/";

const POST_UPDATE_ALIAS = "/api/categoryalias/update"; 
const POST_CREATE_ALIAS = "/api/categoryalias/create"; 
const GET_REMOVE_ALIAS = "/api/categoryalias/delete/";

/* url */
const URL_DOMAIN = window.location.origin;


/* create alias */
$('.c-categories').on('click', '.admin-button-add-alias', function() {
	let alias = {
		id: 0,
		text: 'Default',
		languageId: 1,
		categoryId: +$(this).parents('.category').attr('dataId')
	}
	let child = this;
	$.ajax({
	  dataType: "json",
	  url: URL_DOMAIN + GET_LANGUAGES,
	  data: "",
    success: function(languages) {
		  $.ajax({
		    url: POST_CREATE_ALIAS,
		    datatype: 'json',
		    type: "post", 
		    contentType: "application/json",
		    data: JSON.stringify(alias),
		    success: function(data) {
		    	if (data.successed) {
			    	let aliasJQ = '<div class="alias" dataId="' + data.id + '"><input class="form-control" type="text" value="' + alias.text + '"><select class="custom-select new">';
			    	languages.forEach(function(lang) {
			    		aliasJQ += '<option dataId="' + lang.id + '">' + lang.shortName + '</option>';
			    	})
			    	aliasJQ += '</select><button class="btn btn-primary btn-form admin-button-alias-save">Save alias</button><button class="btn btn-primary btn-form admin-button-alias-remove">Remove alias</button></div>';
						$(child).parent().before(aliasJQ);
						$('.custom-select.new').fancySelect();
					}	else {
		    		alert('Error: ' + data.errorMessages);
					}
		    	console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
		    }
		  });
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
	let category = {
		id: 0,
		Name: 'Default'
	}
  $.ajax({
    url: POST_CREATE_CATEGORY,
    datatype: 'json',
    type: "post", 
    contentType: "application/json",
    data: JSON.stringify(category),
    success: function(data) {
    	if (data.successed) {
				$('.c-categories .categories').append('<div class="category" dataId="' + data.id + '"><h3>Категорія <input id="categoryName" class="form-control" type="text" value="' + category.Name + '"></h3><button class="btn btn-primary btn-form admin-button admin-button-remove-category">Remove category</button><div class="buttons"><button class="btn btn-primary btn-form admin-button-add-alias">Add alias</button><button class="btn btn-primary btn-form admin-button-category-save">Save</button></div></div>');
    	} else {
    		alert('Error: ' + data.errorMessages);
    	}
  		console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
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