(function () {
    $(".demoteUser").on("click", function () {
        $(event.target).addClass("__target");

        $.ajax({
            url: "/en/user/demote",
            type: "POST",
            data: {
                username: $(event.target.closest("tr")).find(".login").text().trim()
            },
            success: function (result) {
                $(".__target").parent().children(".userRoles").text(result.data);
                $(".__target").removeClass("__target");
            }
        });
    });

    $(".promoteUser").on("click", function () {
        $(event.target).addClass("__target");

        $.ajax({
            url: "/en/user/promote",
            type: "POST",
            data: {
                username: $(event.target.closest("tr")).find(".login").text().trim()
            },
			success: function (result) {
                $(".__target").parent().children(".userRoles").text(result.data);
                $(".__target").removeClass("__target");
            }
        });
    });

    $(".banUser").on("click", function() {
        var row = $(event.target.closest("tr")),
			demoteUser = row.find(".demoteUser"),
			promoteUser = row.find(".promoteUser");

        if (demoteUser.hasClass("disabled")) {
            demoteUser.removeClass("disabled");
        } else {
            demoteUser.addClass("disabled");
        }

        if (promoteUser.hasClass("disabled")) {
            promoteUser.removeClass("disabled");
        } else {
            promoteUser.addClass("disabled");
        }
    });
})();