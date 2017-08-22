(function () {
    var bookId = $("#Id").attr("value");

    $.ajax({
        url: "/en/book/GetAuthors/" + bookId,
        type: "POST",
        success: function (result) {
            var authorModelIds = $(".authorModelIds");
            var l = result.data.Authors.length;

            for (var i = 0; i < l; i++) {
				authorModelIds.append("<option value=\"" + result.data.Authors[i].Id + "\" selected=\"selected\">" + result.data.Authors[i].Shortcut + "</option>");
            }

            authorModelIds.prop('disabled', 'disabled');
            authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });

            $(".chosen-container").prop("style", "");
        }
    });
})();