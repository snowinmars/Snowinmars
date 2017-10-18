(function () {
	$.ajax({
		url: "/en/author/getAll",
		type: "POST",
		success: function (data) {
			var authorModelIds = $(".authorModelIds"),
				length = data.data.length;

			for (var i = 0; i < length; i++) {
				authorModelIds.append("<option value=" + data.data[i].Id + ">" + data.data[i].Shortcut + "</option>");
			}

			authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });
		}
	});
})();