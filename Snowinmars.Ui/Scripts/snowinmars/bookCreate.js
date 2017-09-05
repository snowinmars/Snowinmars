(function () {
	$.ajax({
		url: "/en/author/getAll",
		type: "POST",
		success: function (result) {
			var authorModelIds = $(".authorModelIds"),
				length = result.data.length;

			for (var i = 0; i < length; i++) {
				authorModelIds.append("<option value=" + result.data[i].Id + ">" + result.data[i].Shortcut + "</option>");
			}

			authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });
		}
	});
})();