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

			authorModels.chosen({ no_results_text: "Oops, nothing found!" });
		},
		error: function (data) {
			var a = 2;
		}
	});
})();