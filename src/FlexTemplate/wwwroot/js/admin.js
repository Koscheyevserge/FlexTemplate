"use strict"

const POST_UPDATE_CATEGORY = "/api/category/update";
const GET_CREATE_CATEGORY = "/api/category/create"; 
const GET_REMOVE_CATEGORY = "/api/category/delete/";

const GET_CREATE_ALIAS = "/api/category/createalias/";

/* url */
const URL_DOMAIN = window.location.origin;

/* create alias */
$('.c-categories').on('click', '.admin-button-add-alias', function() {
	let child = this;
	$.ajax({
    datatype: 'json',
    url: URL_DOMAIN + GET_CREATE_ALIAS + $(this).parents('.category').attr('dataId'),
    data: "",
    success: function (data) {
      $(child).parent().before(data);                        
     	$('.custom-select.new').fancySelect();
    }
	});
});


/* remove alias */
$('.c-categories').on('click','.admin-button-alias-remove', function() {
	$(this).parent().remove();
});



/* create category */
$('.c-categories').on('click', '.admin-button-add-category', function() {
  $.ajax({
    datatype: 'json',
    url: URL_DOMAIN + GET_CREATE_CATEGORY,
    data: "",
    success: function(data) {
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
	let category = $(this).parents(".category");
	let aliasesParent = category.find(".alias");
	let aliases = [];
	for (let i = 0; i < aliasesParent.length; i++) {
    aliases.push({
			text: $(aliasesParent[i]).find(".form-control").val(),
			languageId: +$(aliasesParent[i]).find('select :selected').attr('dataId'),
			categoryId: +$(aliasesParent[i]).parents('.category').attr('dataId')
		});
	};
	$.ajax({
	  url: URL_DOMAIN + POST_UPDATE_CATEGORY,
    datatype: 'json',
    type: "post", 
    contentType: "application/json",
	  data: JSON.stringify({
			id: +category.attr("dataId"),
			Name: category.find("#categoryName").val(),
			Aliases: aliases
	  }),
		success: function(data) {
    	if (!data.successed) {
    		alert('Error: ' + data.errorMessages);
    	}
			console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
		}
	});
});


// var panelList = $('#draggablePanelList');

// panelList.sortable({
//   // Only make the .panel-heading child elements support dragging.
//   // Omit this to make then entire <li>...</li> draggable.
//   handle: '.panel-heading', 
//   update: function() {
//     $('.panel', panelList).each(function(index, elem) {
// 			var $listItem = $(elem),
// 			   newIndex = $listItem.index();
//     });
//   }
// });