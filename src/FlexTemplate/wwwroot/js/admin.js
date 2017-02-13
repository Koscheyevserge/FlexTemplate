"use strict"

const GET_UPDATE_CATEGORY = "http://localhost:5000/api/category/update";

/* for category */

$('.c-categories .admin-buttom-add-category').click(function() {
	$('.c-categories .categories').append('<div class="category"><h3>Категорія <input class="form-control" type="text" value="Default"></h3><button class="btn btn-primary btn-form admin-button admin-button-remove-category">Remove category</button><div class="alias"><div class="buttons"><button class="btn btn-primary btn-form admin-buttom">Add alias</button><button class="btn btn-primary btn-form admin-buttom">Save</button></div></div></div>');
});

$('.c-categories .admin-button-add-alias').click(function() {
	$(this).parent().before('<div class="alias" id="0"><input class="form-control" type="text" value="Default"><select class="custom-select new"><option>languages</option></select><button class="btn btn-primary btn-form admin-button-alias-remove">Remove alias</button></div>');
	$('.custom-select.new').fancySelect();
});

$('.c-categories .admin-button-alias-remove').click(function() {
	$(this).parent().remove();
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