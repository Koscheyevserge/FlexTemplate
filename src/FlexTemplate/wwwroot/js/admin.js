"use strict"

//category
const POST_UPDATE_CATEGORY = "/api/category/update";
const GET_CREATE_CATEGORY = "/api/category/create"; 
const GET_REMOVE_CATEGORY = "/api/category/delete/";

const GET_CREATE_ALIAS = "/api/category/createalias/";


//page
const POST_UPDATE_PAGES = '/api/page/update';
const GET_CREATE_PAGE_COMPONENT = '/api/page/createcontainer/';

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
    	if (data.successed) {
	      $(child).parent().before(data);                        
	     	$('.custom-select.new').fancySelect();
	    } else {
    		alert('Error: ' + data.errorMessages);
	    }
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
    	if (data.successed) {
	      $('.c-categories .categories').append(data);
	    } else {
    		alert('Error: ' + data.errorMessages);
	    }
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


//pages

/* update pages */
$('.c-pages').on('click', '.admin-button-pages-save', function() {
	let page = $(this).parents(".page");
	let componentsParent = page.find(".component");
	let components = [];
	for (let i = 0; i < componentsParent.length; i++) {
    components.push({
			ContainerTemplateId: +$(componentsParent[i]).attr('dataId'),
			Position: i
		});
	};
	$.ajax({
	  url: URL_DOMAIN + POST_UPDATE_PAGES,
    datatype: 'json',
    type: "post", 
    contentType: "application/json",
	data: JSON.stringify({
		id: +page.attr("dataId"),
		Name: page.find("#pageName").val(),
		BodyClasses: page.find("#pageNameClass").val(),
		Title: page.find("#pageNameTitle").val(),
		PageContainerTemplates: components
	}),
	success: function(data) {
    if (!data.successed) {
    	alert('Error: ' + data.errorMessages);
    }
		console.log("Status: " + data.successed + "\nMessage: " + data.errorMessages);
	}
	});
});

/* create page-component */
$('.c-pages').on('click', '.admin-button-add-component', function() {
  $.ajax({
    datatype: 'json',
    url: URL_DOMAIN + GET_CREATE_PAGE_COMPONENT + $('.c-pages .page').attr('dataId'),
    data: "",
    success: function(data) {
	    $('.c-pages .component-list').append(data);
    }
  });
});

/* remove component */
$('.c-pages').on('click', '.admin-button-remove-component', function() {
	let parent = $(this).parent().remove();
});
