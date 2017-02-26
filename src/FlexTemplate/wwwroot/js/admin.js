"use strict"

const GET_UPDATE_CATEGORY = "http://localhost:5000/api/category/update/";
const GET_REMOVE_ALIAS = "http://localhost:5000/api/categoryalias/delete/";
// const GET_LANGUAGES = "http://localhost:5000/api/languages";

const POST_UPDATE_ALIAS = "http://localhost:5000/api/categoryalias/update"; 
const POST_CREATE_ALIAS = "http://localhost:5000/api/categoryalias/create"; 

/* for category */

$('.c-categories .admin-buttom-add-category').click(function() {
	$('.c-categories .categories').append('<div class="category"><h3>Категорія <input class="form-control" type="text" value="Default"></h3><button class="btn btn-primary btn-form admin-button admin-button-remove-category">Remove category</button><div class="alias"><div class="buttons"><button class="btn btn-primary btn-form admin-buttom">Add alias</button><button class="btn btn-primary btn-form admin-buttom">Save</button></div></div></div>');
});


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
	  url: GET_LANGUAGES,
	  data: "",
    success: function(languages) {
		  $.ajax({
		    url: POST_CREATE_ALIAS,
		    datatype: 'json',
		    type: "post", 
		    contentType: "application/json",
		    data: JSON.stringify(alias),
		    success: function(data) {
		    	let aliasJQ = '<div class="alias" dataId="' + data.id + '"><input class="form-control" type="text" value="' + alias.text + '"><select class="custom-select new">';
		    	languages.forEach(function(lang) {
		    		aliasJQ += '<option dataId="' + lang.id + '">' + lang.shortName + '</option>';
		    	})
		    	aliasJQ += '</select><button class="btn btn-primary btn-form admin-button-alias-save">Save alias</button><button class="btn btn-primary btn-form admin-button-alias-remove">Remove alias</button></div>';
					$(child).parent().before(aliasJQ);
					$('.custom-select.new').fancySelect();
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
	  url: GET_REMOVE_ALIAS + alias.attr('dataId'),
	  data: "",
    success: function(data) {
    	console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
			alias.remove();
    }
	});
});


/* update alias */
$('.c-categories').on('click', '.admin-button-alias-save', function() {
	let alias = $(this).parent();
  $.ajax({
    url: POST_UPDATE_ALIAS,
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
    	console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
    }
  });
});

					    			 		

/* update api */
$('.c-categories .admin-button-save').click(function() {
	let parent = $(this).parents(".category");
	let aliases = [];
	parent.find(".alias").each(function(index) {
		aliases.push({
			Id: +$(this).attr("id"),
			Text: $(this).find(".form-control").val(),
			Language: {
				ShortName: $(this).find(".custom-select").val()
			}
		});
	})
	let data = {
		Id: +parent.attr("id"),
		Name: parent.find("#categoryName").val(),
		Aliases: aliases
	};
	debugger;
	$.ajax({
	  type: "POST",
	  url: GET_UPDATE_CATEGORY,
	  data: data,
	  success: function() {

	  },
	  dataType: dataType
	});
});