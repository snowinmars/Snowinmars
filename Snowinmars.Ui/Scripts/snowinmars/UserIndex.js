(function () {
    $(".emailInput").keyup(function (e) {
        var globalUsernameInputTarget = $(e.target);

        snowinmars_delay(function () {
            $(".emailInputSuccessMessage").addClass("hidden");
            $(".emailInputClientFailMessage").addClass("hidden");
            $(".emailInputServerFailMessage").addClass("hidden");

            if (!globalUsernameInputTarget.val().match(".*\\@.*\\..*")) {
                $(".emailInputClientFailMessage").removeClass("hidden");
            } else {
                $.ajax({
                    url: "/en/user/setEmail/?email=" + globalUsernameInputTarget.val(),
                    type: "POST",
                    success: function (data) {
                        if (data) {
                            $(".emailInputSuccessMessage").removeClass("hidden");
                        } else {
                            $(".emailInputServerFailMessage").removeClass("hidden");
                        }
                    }
                });
            }
        },
            1000);
    });
})();