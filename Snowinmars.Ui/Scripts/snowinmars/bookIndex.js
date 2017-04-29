(function () {
	var authorModelIds = $(".authorModelIds");
	authorModelIds.prop("disabled", "disabled");
	authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });

	$(".chosen-container").prop("style", "");
	$(".chosen-container").addClass("col-xs-12");

	var table = $(".table");

	$(".sk-folding-cube-parent").addClass("hidden");

	table.removeClass("hidden");
	table.DataTable({
		"autoWidth": false 
	});
})();