(function () {
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

	var delay = (function () {
		var timer = 0;

		return function (callback, ms) {
			clearTimeout(timer);
			timer = setTimeout(callback, ms);
		};
	})();

	$("#Username").keyup(function (e) {
		var globalUsernameInputTarget = $(e.target);

		delay(function () {
			
			$.ajax({
				url: "/user/isUsernameExist/?username=" + globalUsernameInputTarget.val(),
				type: "POST",
				success: function (data) {
					if (data) {
						$(".oldUserHello").removeClass("hidden");
						$("#Password").prop("disabled", false);
					} else {
						$(".newUserHello").removeClass("hidden");
						$("#Password").prop("disabled", false);
						$("#PasswordConfirm").prop("disabled", false);
						$("#PasswordConfirm").closest(".form-group").removeClass("hidden");
					}
				}
			});

		}, 300);
	});
})();