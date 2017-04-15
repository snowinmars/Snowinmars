(function () {
	var bookId = $(".dl-horizontal").data().id;

	$.ajax({
		url: "/book/GetAuthors/" + bookId,
		type: "POST",
		success: function (data) {
			var authorModelIds = $(".authorModelIds");


			var options = authorModelIds.children("option");
			var l = data.length;
			var author;

			for (var i = 0; i < l; i++) {
				authorModelIds.append("<option value=\"" + data[i].Id + "\" selected=\"selected\">" + data[i].Shortcut + "</option>");
			}

			authorModelIds.prop('disabled', 'disabled');
			authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });
		}
	});
})();