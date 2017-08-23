﻿var renderEngine = SnowinmarsGlobal.namespace("SnowinmarsGlobal.renderEngine");

renderEngine.showNothingFoundMessage = function () {
	$(".result").prepend("<div>Nothigs equals to exactly 40</div>");
	$(".result").prepend("<hr />");
}

renderEngine.showErrorMessage = function () {
	$(".result").prepend("<div>Error was occured processing the request</div>");
	$(".result").prepend("<hr />");
}

renderEngine.showOverflowErrorMessage = function () {
	$(".overflowErrorHint").removeClass("hidden");
}

renderEngine.showOverflowImportErrorHint = function () {
	$(".overflowImportErrorHint").removeClass("hidden");
}

renderEngine.showQualitiesList = function (qualities, data) {
	var list = "<div>Input qualities is ",
		length = qualities.length,
		i,
		completeQualitiesCount = 0,
		row;

	for (i = 0; i < length; i++) {
		list += qualities[i].value + " ";
	}

	list += "</div>" +
		"<div>You will get 1 orb for each list item below</div>" +
		"<ul>" +
		'<li class="completeQualitiesCount"></li>';

	length = data.length;

	for (i = 0; i < length; i++) {
		row = data[i];

		if (row[0] === 20) {
			completeQualitiesCount++;
		} else {
			list += "<li>" + row.join(" + ") + "</li>";
		}
	}

	list += "</ul>";

	$(".result").prepend(list);

	if (completeQualitiesCount > 0) {
		$(".completeQualitiesCount").text("20 × " + completeQualitiesCount);
	} else {
		$(".completeQualitiesCount").addClass("hidden");
	}
	$(".result").prepend("<hr />");
}