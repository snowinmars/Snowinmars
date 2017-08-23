(function () {
	var config = SnowinmarsGlobal.config,
		renderEngine = SnowinmarsGlobal.renderEngine;

	$(".importBtn").on("click",
		function () {
			var textarea = $(".importExportTextarea"),
				importData = textarea.val(),
				arr = importData.trim().split(" "),
				length = arr.length,
				qualitiesGroup = $(".qualitiesGroup"),
				qualityInputHtml = config.qualityInputHtml,
				i;

			qualitiesGroup.empty();

			if (length > 20) {
				length = 20;
				renderEngine.showOverflowImportErrorHint();
			}

			for (i = 0; i < length; i++) {
				var wrapper = document.createElement("span");
				wrapper.innerHTML = qualityInputHtml;
				var q = wrapper.firstChild;
				$(q).val(arr[i]);

				qualitiesGroup.append(q);
			}

			if (qualitiesGroup.children().length <= 17) {
				qualitiesGroup.append(qualityInputHtml);
				qualitiesGroup.append(qualityInputHtml);
				return;
			}

			if (qualitiesGroup.children().length === 18) {
				renderEngine.showOverflowErrorMessage();
				qualitiesGroup.append(qualityInputHtml);
				qualitiesGroup.append(qualityInputHtml);
				return;
			}

			if (qualitiesGroup.children().length === 19) {
				renderEngine.showOverflowErrorMessage();
				qualitiesGroup.append(qualityInputHtml);
				return;
			}

			if (qualitiesGroup.children().length >= 20) {
				renderEngine.showOverflowErrorMessage();
				return;
			}
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
})();