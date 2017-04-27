(function () {
	var bookId = $("#Id").attr("value");

	$.ajax({
		url: "/book/GetAuthors/" + bookId,
		type: "POST",
		success: function (data) {
			var authorModels = $(".authorModels");
			var l = data.Authors.length;

			for (var i = 0; i < l; i++) {
				authorModels.append("<option value=\"" + data.Authors[i].Id + "\" selected=\"selected\">" + data.Authors[i].Shortcut + "</option>");
			}

			authorModels.prop('disabled', 'disabled');
			authorModels.chosen({ no_results_text: "Oops, nothing found!" });
		}
	});
})();