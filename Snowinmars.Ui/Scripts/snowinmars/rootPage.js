(function () {
	$("#shortcutJobForm").submit(function (e) {
		e.preventDefault();

		var jobName = $(e.currentTarget).find("#ShortcutJobName").val(),
			entropy = $(e.currentTarget).find("#ShortcutJobSmtpEntropy").val();

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

	$("#warningJobForm").submit(function (e) {
		e.preventDefault();

		var jobName = $(e.currentTarget).find("#WarningJobName").val(),
			entropy = $(e.currentTarget).find("#WarningJobSmtpEntropy").val();

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
					button = $("#warningJobForm").find(".glyphicon-off");

					button.removeClass("fail");
					button.removeClass("success");

					button.addClass("success");
				} else {
					button = $("#warningJobForm").find(".glyphicon-off");

					button.removeClass("fail");
					button.removeClass("success");

					button.addClass("fail");
				}
			},
			error: function () {
				var button = $("#warningJobForm").find(".glyphicon-off");

				button.removeClass("fail");
				button.removeClass("success");

				button.addClass("fail");
			}
		});
	});

	$("#emailServiceForm").submit(function (e) {
		e.preventDefault();

		var jobName = $(e.currentTarget).find("#EmailServiceName").val(),
			entropy = $(e.currentTarget).find("#EmailServiceSmtpEntropy").val();

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
					button = $("#emailServiceForm").find(".glyphicon-off");

					button.removeClass("fail");
					button.removeClass("success");

					button.addClass("success");
				} else {
					button = $("#emailServiceForm").find(".glyphicon-off");

					button.removeClass("fail");
					button.removeClass("success");

					button.addClass("fail");
				}
			},
			error: function () {
				var button = $("#emailServiceForm").find(".glyphicon-off");

				button.removeClass("fail");
				button.removeClass("success");

				button.addClass("fail");
			}
		});
	});
})();