(function () {
    $.ajax({
        url: "/en/author/getAll",
        type: "POST",
        success: function (data) {
            var authorModelIds = $(".authorModelIds"),
	            length = data.length;

            for (var i = 0; i < length; i++) {
                authorModelIds.append("<option value=" + data[i].Id + ">" + data[i].Shortcut + "</option>");
            }

            authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });
        }
    });
})();