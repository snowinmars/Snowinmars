(function () {
	$.ajax({
		url: "/author/getAll",
		type: "POST",
		success: function (data) {
			var authorModelIds = $(".authorModelIds");
			var l = data.length;

			for (var i = 0; i < l; i++) {
				authorModelIds.append("<option value=" + data[i].Id + ">" + data[i].Shortcut + "</option>");
			}

			authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });
		}
	});
})();