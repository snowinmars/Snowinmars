var SnowinmarsGlobal = SnowinmarsGlobal || {};

SnowinmarsGlobal.namespace = function (namespaceString) {
	var parts = namespaceString.split("."),
		parent = SnowinmarsGlobal,
		i;

	if (parts[0] === "SnowinmarsGlobal") {
		parts = parts.slice(1);
	}

	for (i = 0; i < parts.length; i += 1) {
		if (typeof parent[parts[i]] === "undefined") {
			parent[parts[i]] = {};
		}

		parent = parent[parts[i]];
	}

	return parent;
};

(function () {
	var navbar = $(".navbar-nav"),
		urlParts = location.pathname.split("/"),
		urlPartsLength = urlParts.length,
		urlPart,
		i;

	$("[data-toggle='tooltip']").tooltip();

	/////////////

	navbar.children().removeClass("active");

	/////////////

	// toggle navbar buttons
	for (i = 0; i < urlPartsLength; i++) {
		urlPart = urlParts[i];

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
		})(),
			checkUsernameFunction = function (e) {
				var globalUsernameInputTarget = $(e.target);

				snowinmarsDelay(function () {
					$.ajax({
						url: "/en/user/isUsernameExist/?username=" + globalUsernameInputTarget.val(),
						type: "POST",
						success: function (data) {
							if (data.data) { // if we have user with this login
								$(".oldUserHello").removeClass("hidden");
								$(".newUserHello").addClass("hidden");

								$("#Password").prop("disabled", false); // we have to show the password input
								$("#PasswordConfirm").prop("disabled", true); // but haven't show the password confirm
								$("#PasswordConfirm").closest(".form-group").addClass("hidden");
							} else { // if we don't know this user
								if (!$("#Username").val()) { // if he didn't enter login
									$(".oldUserHello").addClass("hidden"); // we hide all password inputs
									$(".newUserHello").addClass("hidden");

									$("#PasswordConfirm").prop("disabled", true);
									$("#PasswordConfirm").closest(".form-group").addClass("hidden");
								} else { // otherwise
									$(".newUserHello").removeClass("hidden"); // we allow to type password and password confirm
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

		$("#enterModal").on("shown.bs.modal", function () {
			$("#Username").focus();
		});

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
						},
						success: function (result) {
							if (result.success) {
								location.href = result.data.Redirect;
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
					$.ajax({
						url: "/en/User/Enter",
						type: "POST",
						data: {
							username: $(e.target).find("#Username").val(),
							password: $(e.target).find("#Password").val(),
							PasswordConfirm: $(e.target).find("#PasswordConfirm").val()
						},
						success: function (result) {
							if (result.success) {
								location.href = result.data.Redirect;
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

			$(".oldUserHello").addClass("hidden");
			$(".newUserHello").addClass("hidden");
			$(".errorOnLoginProcess").removeClass("hidden");
		});
	}

	setupEnterModalWindow();
})();