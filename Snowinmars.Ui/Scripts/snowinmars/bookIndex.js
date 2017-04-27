(function () {

	$("#authorTable").DataTable();

	

		$.ajax({
			url: "/book/selectBookIdWithAuthors/",
			type: "POST",
			success: function (data) {
				var books = $(".book");
				var booksLength = books.length;

				var parsedData = $.parseJSON(data);

				for (var j = 0; j < booksLength; j++) {
					var authorModels = $("[data-id=" + data.BookId + "]").find(".authorModels");
					var authorsLength = data.Authors.length;

					for (var i = 0; i < authorsLength; i++) {
						authorModels.append("<option value=\"" + data.Authors[i].Id + "\" selected=\"selected\">" + data.Authors[i].Shortcut + "</option>");
					}

					authorModels.prop("disabled", "disabled");
					authorModels.chosen({ no_results_text: "Oops, nothing found!" });
				}
				//var authorModels = $("[data-id=" + data.BookId + "]").find(".authorModels");
				//var l = data.Authors.length;

				//for (var i = 0; i < l; i++) {
				//	authorModels.append("<option value=\"" + data.Authors[i].Id + "\" selected=\"selected\">" + data.Authors[i].Shortcut + "</option>");
				//}
				//authorModels.prop("disabled", "disabled");
				//authorModels.chosen({ no_results_text: "Oops, nothing found!" });

				//authorModels.trigger("chosen:updated");
			}
		});
	

	$("#authorTable_wrapper>div:first-child>div").addClass("col-xs-6");

	
	//$(document).on("keyup", function (evt) {
	//	if (evt.keyCode === 78) {
	//		location.href = "/Book/Create";
	//	}
	//});
})();