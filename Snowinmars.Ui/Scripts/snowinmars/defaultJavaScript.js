(function () {
    $("[data-toggle='tooltip']").tooltip();
   
    var navbar = $(".navbar-nav");
    navbar.children().removeClass("active");

    // toggle navbar buttons
    var urlParts = location.pathname.split("/");
    var urlPartsLength = urlParts.length;
    for (var i = 0; i < urlPartsLength; i++) {
        var urlPart = urlParts[i];

        switch (urlPart.toLocaleLowerCase()) {
            case "book":
			case "author":
                navbar.children("[data-id=book]").addClass("active");
				break;
			case "pathofexile":
				navbar.children("[data-id=pathofeilxe]").addClass("active");
				break;
        }
    }

    function setupEnterModalWindow() {
        var snowinmarsDelay = (function () {
            var timer = 0;

            return function (callback, ms) {
                clearTimeout(timer);
                timer = setTimeout(callback, ms);
            };
        })();

        $("#enterModal").on("shown.bs.modal", function () {
            $("#Username").focus();
        });

        var checkUsernameFunction = function (e) {
            var globalUsernameInputTarget = $(e.target);

            snowinmarsDelay(function () {
                $.ajax({
                    url: "/en/user/isUsernameExist/?username=" + globalUsernameInputTarget.val(),
                    type: "POST",
                    success: function (data) {
                        if (data) {
                            $(".oldUserHello").removeClass("hidden");
                            $(".newUserHello").addClass("hidden");

                            $("#Password").prop("disabled", false);
                            $("#PasswordConfirm").prop("disabled", true);
                            $("#PasswordConfirm").closest(".form-group").addClass("hidden");
                        } else {
                            if (!$("#Username").val()) {
                                $(".oldUserHello").addClass("hidden");
                                $(".newUserHello").addClass("hidden");

                                $("#PasswordConfirm").prop("disabled", true);
                                $("#PasswordConfirm").closest(".form-group").addClass("hidden");
                            } else {
                                $(".newUserHello").removeClass("hidden");
                                $(".oldUserHello").addClass("hidden");

                                $("#Password").prop("disabled", false);
                                $("#PasswordConfirm").prop("disabled", false);
                                $("#PasswordConfirm").closest(".form-group").removeClass("hidden");
                            }
                        }
                    },
                    error: function (data) {
                        $(".oldUserHello").addClass("hidden");
                        $(".newUserHello").addClass("hidden");
                        $(".errorOnLoginProcess").removeClass("hidden");
                    }
                });
            },
                300);
        };

        $("#Username").on("keyup", checkUsernameFunction);
        $("#Username").on("blur", checkUsernameFunction);

        $(".spoiler-trigger").click(function () {
            $(this).parent().next().collapse("toggle");
        });

        $("#ForgotPasswordAdminMessageSubmit").on("click", function () {
            $.ajax({
                url: "/en/home/emailAdmin",
                type: "POST",
                data: {
                    message: $("#ForgotPasswordAdminMessage").val()
                },
                success: function (data) {
                    if (data) {
                        $(".spoiler-trigger").click();
                        $(".spoiler-trigger").addClass("success");

                        var currentLanguage = location.pathname.split("/")[1];
                        switch (currentLanguage) {
                            case "ru":
                                $(".spoiler-trigger").text("Отправлено");
                                break;
                            default:
                            case "en":
                                $(".spoiler-trigger").text("Sended");
                                break;
                        }

                        $(".spoiler-trigger").unbind();
                    } else {
                        $("#ForgotPasswordAdminMessageSubmit").addClass("btn-danger");
                    }
                },
                error: function (data) {
                    $("#ForgotPasswordAdminMessageSubmit").addClass("btn-danger");
                }
            });
        });

        $("#EnterForm").on("submit", function (e) {
            e.preventDefault();

            if (!($(".oldUserHello").hasClass("hidden"))) {
				if ($("#Username").val() && $("#Password").val()) {
					$.ajax({
						url: "/en/User/Enter",
						type: "POST",
						data: {
							username: $(e.target).find("#Username").val(),
							password: $(e.target).find("#Password").val(),
							PasswordConfirm: $(e.target).find("#PasswordConfirm").val()
						},
						success: function (data) {
							if (data.Success) {
								location.href = data.Redirect;
							} else {
								$(".errorOnLoginProcess").removeClass("hidden");
							}
						},
						error: function (data) {
							$(".errorOnLoginProcess").removeClass("hidden");
						}
					});
                    return;
                }
            }

            if (!($(".newUserHello").hasClass("hidden"))) {
                if ($("#Username").val() && $("#Password").val() && $("#PasswordConfirm").val()) {
                    e.target.submit();
                    return;
                }
            }

            $(".oldUserHello").addClass("hidden");
            $(".newUserHello").addClass("hidden");
            $(".errorOnLoginProcess").removeClass("hidden");
        });
    }

    setupEnterModalWindow();
})();