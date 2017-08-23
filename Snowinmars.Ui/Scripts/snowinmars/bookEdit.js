(function () {
	$.ajax({
		url: "/en/author/getAll",
		type: "POST",
		success: function (result) {
			var authorModelIds = $(".authorModelIds"),
				length = result.data.length,
				bookId = $("#Id").attr("value"),
				i;

			for (i = 0; i < length; i++) {
				authorModelIds.append("<option value=" + result.data[i].Id + ">" + result.data[i].Shortcut + "</option>");
			}

			$.ajax({
				url: "/en/book/GetAuthors/" + bookId,
				type: "POST",
				success: function (result) {
					var authorModelIds = $(".authorModelIds"),
						l = result.data.Authors.length,
						i;

					for (i = 0; i < l; i++) {
						authorModelIds.children("[value=" + result.data.Authors[i].Id + "]").attr("selected", "selected");
					}

					authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });

					$(".chosen-container").prop("style", "");
				}
			});
		}
	});

	$(".btn-closeUrlModal").click(function (e) {
		$(e.target.parentElement.parentElement).find("input").val("");
	});

	$(".btn-okUrlModal").click(function (e) {
		if (!($(e.target.parentElement.parentElement).find("input").valid())) {
			return false;
		}
	});
})();