(function () {
    var authorModelIds = $(".authorModelIds");
    authorModelIds.prop("disabled", "disabled");
    authorModelIds.chosen({
        no_results_text: "Oops, nothing found!",
        inherit_select_classes: true
    });

    $(".chosen-container").prop("style", "");

    var table = $(".table");

    $(".sk-folding-cube-parent").addClass("hidden");

    table.removeClass("hidden");
    table.DataTable({
        columnDefs: [{ targets: "no-sort", orderable: false }],
        "autoWidth": false
    });

    var rows = $(".synchronizationIcon:not(.hidden)").closest("tr");
    var rowsLength = rows.length;

    if (rowsLength > 0) {
        global__refreshIntervalId = setInterval(function () {
            for (var i = 0; i < rowsLength; i++) {
                $.ajax({
                    url: "/en/Book/Details/" + $(rows[i]).data().id,
                    type: "POST",
                    success: function (data) {
                        if (data.IsSynchronized) {
                            var row = $("tr[data-id='" + data.Id + "']");

                            row.find(".synchronizationIcon").addClass("hidden");

                            var select = row.find("select.authorModelIds");

                            var idsLength = data.AuthorShortcuts.length;
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