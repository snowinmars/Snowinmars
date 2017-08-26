(function () {
	function update() {
		$(".successMessage").addClass("hiddenElement");
		$(".serverFailMessage").addClass("hiddenElement");

		$.ajax({
			url: "/en/user/update",
			type: "POST",
			dataType: "json",
			data: {
				'model': {
					'Id': $("#HiddenId").val(),
					'Username': $("#HiddenUsername").val(),
					'Roles': $("#HiddenRoles").val(),
					'Email': $("#Email").val(),
					'Language': $("#Language").val(),
				}
			},
			success: function () {
				$(".successMessage").removeClass("hiddenElement");

				setTimeout(function () {
					$(".successMessage").addClass("hiddenElement");
				}, 3000);
			},
			error: function () {
				$(".serverFailMessage").removeClass("hiddenElement");

				setTimeout(function () {
					$(".serverFailMessage").addClass("hiddenElement");
				}, 3000);
			}
		});
	}

	$("#Email").keyup(function (e) {
		snowinmars_delay(function () {
			update();
		},
			1000);
	});

	$("#Language").on("change", function (e) {
		update();
	});
})();