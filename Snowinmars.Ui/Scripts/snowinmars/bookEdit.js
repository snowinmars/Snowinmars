(function () {
    $.ajax({
        url: "/en/author/getAll",
        type: "POST",
        success: function (data) {
            var authorModelIds = $(".authorModelIds");
            var l = data.length;

            for (var i = 0; i < l; i++) {
                authorModelIds.append("<option value=" + data[i].Id + ">" + data[i].Shortcut + "</option>");
            }

            var bookId = $("#Id").attr("value");

            $.ajax({
                url: "/en/book/GetAuthors/" + bookId,
                type: "POST",
                success: function (data) {
                    var authorModelIds = $(".authorModelIds");
                    var l = data.Authors.length;

                    for (var i = 0; i < l; i++) {
                        authorModelIds.children("[value=" + data.Authors[i].Id + "]").attr("selected", "selected");
                    }

                    authorModelIds.chosen({ no_results_text: "Oops, nothing found!" });

                    $(".chosen-container").prop("style", "");
                }
            });
        }
    });

    $(".btn-closeUrlModal").click(function (e) {
        $(e.target.parentElement.parentElement).find("input").val("");
    });

    $(".btn-okUrlModal").click(function (e) {
        if (!($(e.target.parentElement.parentElement).find("input").valid())) {
            return false;
        }
    });
})();