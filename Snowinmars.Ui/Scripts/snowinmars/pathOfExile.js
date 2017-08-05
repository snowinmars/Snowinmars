(function () {
	$(".clearQualities").on("click",
		function() {
			$.each($(".quality"),
				function(index, value) {
					value.value = "";
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

		// 10 is limit for bll

		if ($("#qualitiesGroup").children().length >= 10) {
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
				desiredValue: 40,
			},
			success: function (data) {

				var length = data.length;
				var list = "<ul>";

				for (var i = 0; i < length; i++) {
					var row = data[i];

					list += "<li>";

					list += row.join(" + ");

					list += "</li>";
				}

				list += "</ul>";

				$(".result").append(list);
				
			},
			error: function (data) {
				$(".result").append("<div>Nothigs equals to exactly 40</div>");
			}
		});
	});
})();