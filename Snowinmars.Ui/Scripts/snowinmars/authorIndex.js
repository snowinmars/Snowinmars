(function () {
	var table = $(".table");

	$(".sk-folding-cube-parent").addClass("hidden");

	table.removeClass("hidden");
	table.DataTable({
		columnDefs: [{ targets: "no-sort", orderable: false }]
	});

	table.find("th").prop("style", "");
})();