(function () {
	var books = $(".book");
	var l = books.length;

	for (var j = 0; j < l; j++) {
		var bookId = $(books[j]).data().id;

		$.ajax({
			url: "/book/GetAuthors/" + bookId,
			type: "POST",
			success: function (data) {
				var authorModelIds = $("[data-id=" + data.BookId + "]").find(".authorModelIds");
				var l = data.Authors.length;

				for (var i = 0; i < l; i++) {
					authorModelIds.append("<option value=\"" + data.Authors[i].Id + "\" selected=\"selected\">" + data.Authors[i].Shortcut + "</option>");
				}

				authorModelIds.prop("disabled", 'disabled');
				authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });
			}
		});
	}
})();