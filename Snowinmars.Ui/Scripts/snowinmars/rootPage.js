(function () {
    $("#shortcutJobForm").submit(function (e) {
        e.preventDefault();

        var jobName = $(e.currentTarget).find("#ShortcutJobName").val();
        var entropy = $(e.currentTarget).find("#ShortcutJobSmtpEntropy").val();

        $.ajax({
            url: "/en/user/setSmtpEntropies",
            type: "POST",
            dataType: "json",
            data: {
                'jobName': jobName,
                'entropy': entropy,
            },
            success: function (data) {
                var button;

                if (data) {
                    button = $("#shortcutJobForm").find(".glyphicon-off");

                    button.removeClass("fail");
                    button.removeClass("success");

                    button.addClass("success");
                } else {
                    button = $("#shortcutJobForm").find(".glyphicon-off");

                    button.removeClass("fail");
                    button.removeClass("success");

                    button.addClass("fail");
                }
            },
            error: function () {
                var button = $("#shortcutJobForm").find(".glyphicon-off");

                button.removeClass("fail");
                button.removeClass("success");

                button.addClass("fail");
            }
        });
    });
})();