(function () {
	$(".clearQualities").on("click",
		function() {
			$.each($(".quality"),
				function(index, value) {
					value.value = "";
				});
		});

	$(".clearResultSetBtn").on("click",
		function() {
			$(".result").empty();
		});

	$(".importBtn").on("click",
		function() {

		});

	$(".exportBtn").on("click",
		function () {
			var textarea = $(".importExportTextarea");

			textarea.val("");

			$.each($(".quality"),
				function (index, value) {
					textarea.val(textarea.val() + " " + value.value);
				});
		});

	var qualityInputHtml = '<input class="flex-item quality col-xs-2" type="number" step="1" min="1" max="20"/>';

	$("#qualitiesGroup").on("blur", ".quality", function () {
		// scicers for this value
		if (this.value) {
			var max = +$(this).attr("max");
			if (this.value > max) {
				$(this).val(max);
			}

			var min = +$(this).attr("min");
			if (this.value < min) {
				$(this).val(min);
			}
		}

		if ($("#qualitiesGroup").children().length >= 20) {
			$(".overflowErrorHint").removeClass("hidden");
			return;
		}

		// user don't need more then two empty inputs

		var emptyQualityInputCount = 0;
		$.each($("#qualitiesGroup").children(),
			function(index, value) {
				if (!value.value) {
					emptyQualityInputCount ++;
				}
			});

		if (emptyQualityInputCount >= 2) {
			return;
		}

		// add new input

		$(".overflowErrorHint").addClass("hidden");

		if (this.value.trim()) {
			$("#qualitiesGroup").append(qualityInputHtml);
		}
	});

	$(".qualitiesForm").on("submit", function(e) {
		e.preventDefault();

		var arr = [];
		$.each($("#qualitiesGroup").children(),
			function (index, value) {
				if (value.value) {
					arr.push(value.value);
				}
			});

		$.ajax({
			url: "/en/pathOfExile/qualities",
			type: "POST",
			data: {
				qualities: arr,
				desiredValue: 40
			},
			success: function (result) {
				if (result.data === null) {
					$(".result").prepend("<div>Nothigs equals to exactly 40</div>");
					$(".result").prepend("<hr />");
					return;
				}

				var list = "<span>Input qualities is ";

				var qualities = $(".quality");

				var length = qualities.length;

				for (var i = 0; i < length; i++) {
					list += qualities[i].value + " ";
				}

				list += "</span>";

				list += '<ul>';

				length = result.data.length;
				for (var i = 0; i < length; i++) {
					var row = result.data[i];

					list += "<li>";

					list += row.join(" + ");

					list += "</li>";
				}

				list += "</ul>";

				$(".result").prepend(list);
				$(".result").prepend("<hr />");
			},
			error: function (data) {
				$(".result").prepend("<div>Error was occured processing the request</div>");
				$(".result").prepend("<hr />");
			}
		});
	});
})();