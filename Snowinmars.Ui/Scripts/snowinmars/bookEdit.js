(function () {
	$.ajax({
		url: "/author/getAll",
		type: "POST",
		success: function (data) {
			var authorModels = $(".authorModels");
			var l = data.length;

			for (var i = 0; i < l; i++) {
				authorModels.append("<option value=" + data[i].Id + ">" + data[i].Shortcut + "</option>");
			}

			var bookId = $("#Id").attr("value");

			$.ajax({
				url: "/book/GetAuthors/" + bookId,
				type: "POST",
				success: function (data) {
					var authorModels = $(".authorModels");
					var l = data.Authors.length;

					for (var i = 0; i < l; i++) {
						authorModels.children("[value=" + data.Authors[i].Id + "]").attr("selected", "selected");
					}

					authorModels.chosen({ no_results_text: "Oops, nothing found!" });
				}
			});
		}
	});
})();