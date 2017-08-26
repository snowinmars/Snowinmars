(function () {
	var qualityInputHtml = SnowinmarsGlobal.config.qualityInputHtml,
		renderEngine = SnowinmarsGlobal.renderEngine;

	$(".qualitiesGroup").on("blur", ".quality", function () {
		var max,
			min,
			emptyQualityInputCount = 0,
			qualitiesGroup = $(".qualitiesGroup");

		// scicers for this value
		if (this.value) {
			max = +$(this).attr("max");
			if (this.value > max) {
				$(this).val(max);
			}

			min = +$(this).attr("min");
			if (this.value < min) {
				$(this).val(min);
			}
		}

		if (qualitiesGroup.children().length >= 20) {
			$(".overflowErrorHint").removeClass("hidden");
			return;
		}

		// user don't need more then two empty inputs

		$.each(qualitiesGroup.children(),
			function (index, value) {
				if (!value.value) {
					emptyQualityInputCount++;
				}
			});

		if (emptyQualityInputCount >= 2) {
			return;
		}

		// add new input

		$(".overflowErrorHint").addClass("hidden");

		if (this.value.trim()) {
			qualitiesGroup.append(qualityInputHtml);
		}
	});

	$(".qualitiesForm").on("submit", function (e) {
		e.preventDefault();

		var arr = [];
		$.each($(".qualitiesGroup").children(),
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
					renderEngine.showNothingFoundMessage();
					return;
				}

				renderEngine.showQualitiesList($(".quality"), result.data);
			},
			error: function (data) {
				renderEngine.showErrorMessage();
			}
		});
	});
})();