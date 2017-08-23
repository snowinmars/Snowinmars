(function () {
	var authorModelIds = $(".authorModelIds"),
		rows = $(".synchronizationIcon:not(.hidden)").closest("tr"),
		rowsLength = rows.length;

	authorModelIds.prop("disabled", "disabled");
	authorModelIds.chosen({
		no_results_text: "Oops, nothing found!",
		inherit_select_classes: true
	});

	$(".chosen-container").prop("style", "");

	if (rowsLength > 0) {
		global__refreshIntervalId = setInterval(function () {
			for (var i = 0; i < rowsLength; i++) {
				$.ajax({
					url: "/en/Book/Details/" + $(rows[i]).data().id,
					type: "POST",
					success: function (data) {
						if (data.IsSynchronized) {
							var row = $("tr[data-id='" + data.Id + "']"),
								select,
								idsLength = data.AuthorShortcuts.length;

							row.find(".synchronizationIcon").addClass("hidden");

							select = row.find("select.authorModelIds");

							for (var j = 0; j < idsLength; j++) {
								select.append($("<option>", {
									text: data.AuthorShortcuts[j],
									selected: "selected"
								}));
							}

							select.trigger("chosen:updated");

							clearInterval(global__refreshIntervalId);
						}
					}
				});
			}
		},
			2000);
	}
})();