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

		$(".submitLoadIcon").removeClass("hidden");
		$(".submitButton").addClass("disabled");
		
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
				var d = JSON.parse(result.data),
					p = [],
					pair;

				if (d === null ||
					d === undefined ||
					d.length === 0 ||
					d[0] === null ||
					d[0].length === 0) {
					renderEngine.showNothingFoundMessage();
					$(".submitLoadIcon").addClass("hidden");
					$(".submitButton").removeClass("disabled");
					return;
				}

				// object to array
				for (pair in d) {
					p.push([pair, d[pair]]);
				}

				// sort dictionary by ValueArray.Count
				p.sort(function (lhs, rhs) {
					if (lhs[1].length > rhs[1].length) {
						return -1;
					}

					if (lhs[1].length < rhs[1].length) {
						return 1;
					}

					return 0;
				});

				renderEngine.showQualitiesList($(".quality"), p);
			},
			error: function (data) {
				renderEngine.showErrorMessage();
			}
		});
	});
})();