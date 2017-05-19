(function() {
	$("[data-toggle='tooltip']").tooltip();

	// toggle navbar buttons

	var parser = document.createElement("a");
	parser.href = location.href;
	var page = parser.pathname.split("/")[1];
	var navbar = $(".navbar-nav");
	navbar.children().removeClass("active");

	switch (page) {
	case "author":
	case "Author":
		navbar.children("[data-id=author]").addClass("active");
		break;
	case "book":
	case "Book":
		navbar.children("[data-id=book]").addClass("active");
		break;
	}

	$("#Username").keyup(function(e) {
		var globalUsernameInputTarget = $(e.target);

		snowinmars_delay(function () {

				$.ajax({
					url: "/user/isUsernameExist/?username=" + globalUsernameInputTarget.val(),
					type: "POST",
					success: function (data) {
                        if (data) {
                            $(".oldUserHello").removeClass("hidden");
                            $(".newUserHello").addClass("hidden");

                            $("#Password").prop("disabled", false);
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
				});

			},
			300);
	});
})();

var snowinmars_delay = (function () {
	var timer = 0;

	return function (callback, ms) {
		clearTimeout(timer);
		timer = setTimeout(callback, ms);
	};
})();