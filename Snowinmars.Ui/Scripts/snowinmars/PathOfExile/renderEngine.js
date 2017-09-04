var renderEngine = SnowinmarsGlobal.namespace("SnowinmarsGlobal.renderEngine"),
	config = SnowinmarsGlobal.config;

renderEngine.insertQualityInput = function(wrapper) {
	wrapper.innerHTML = qualityInputHtml;
	$(".toSelect").each(renderEngine.convertToSelect);
	$(".toInput").each(renderEngine.convertToInput);
}

renderEngine.convertToSelect = function() {
	var $this = $(this),
		select = '<select class="toInput">',
		i;

	for (i = 1; i <= 20; i++) {
		select += '<option value="' + i + '">' + i + "</option>";
	}

	select += "</select>";

	$this.after(select);

	$this.remove();
};

renderEngine.convertToInput = function() {
	var $this = $(this),
		numberOfOptions = $(this).children("option").length,
		$styledSelect,
		$list,
		i,
		$listItems;

	$this.wrap('<div class="select col-xs-2"></div>');

	$this.after(
		'<input class="styledSelect flex-item quality col-xs-12" type="number" step="1" min="1" max="20"></input>');

	$styledSelect = $this.next(".styledSelect");

	$styledSelect.text($this.children("option").eq(0).text());

	$list = $("<ul />",
		{
			'class': "options col-xs-12"
		}).insertAfter($styledSelect);

	for (i = 0; i < numberOfOptions; i++) {
		$("<li />",
			{
				class: "col-xs-3",
				text: $this.children("option").eq(i).text(),
				rel: $this.children("option").eq(i).val()
			}).appendTo($list);
	}

	$this.remove();

	$listItems = $list.children("li");

	// Show the unordered list when the styled div is clicked (also hides it if the div is clicked again)
	$styledSelect.on("click", function(e) {
		e.stopPropagation();

		var $this = $(this);

		$(".styledSelect.activeInput").each(function() {
			$this.removeClass("activeInput").next(".options").hide();
		});
		$this.toggleClass("activeInput").next("ul.options").toggle();
	});

	$styledSelect.on("focus", function (e) {
		e.stopPropagation();
		$(".styledSelect.activeInput").each(function () {
			$(this).removeClass("activeInput").next(".options").hide();
		});
	});

	// Hides the unordered list when a list item is clicked and updates the styled div to show the selected list item
	// Updates the select element to have the value of the equivalent option
	$listItems.on("click", function(e) {
		e.stopPropagation();

		var $this = $(this);

		$styledSelect.text($this.text());
		$styledSelect.val($this.text());

		$list.hide();
		$styledSelect.removeClass("activeInput");
	});

	// Hides the unordered list when clicking outside of it
	$(document).click(function () {
		$styledSelect.removeClass("activeInput");
		$list.hide();
	});
};

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
	var list = '<div>Input qualities is <span class="inputQuality">',
		length = qualities.length,
		i,
		j,
		completeQualitiesCount = 0,
		groupslength = data.length;

	for (i = 0; i < length; i++) {
		list += qualities[i].value + " ";
	}

	list += '</span></div>' +
		"<div>You will get 1 orb for each list item below</div>" +
		"<ul>" +
		'<li class="completeQualitiesCount"></li>';

	list += '<li class="qualityGroupLists">';

	for (i = 0; i < groupslength; i++) { // first group is 20% qualities
		list += '<ul class="qualityGroupList"><li>Group ' + i + "</li><li><ul>";

		for (j = 0; j < data[i][1].length; j++) {
			if (data[i][1][j][0] === 20) {
				completeQualitiesCount++;
			} else {
				list += "<li>" + data[i][1][j].join(" + ") + "</li>";
			}
		}

		list += "</ul></li></ul>";
	}

	list += "</li>";

	list += "</ul>";

	$(".result").prepend(list);

	if (completeQualitiesCount > 0) {
		$(".completeQualitiesCount").text("20 × " + completeQualitiesCount);
	} else {
		$(".completeQualitiesCount").addClass("hidden");
	}
	$(".result").prepend("<hr />");

	$(".submitLoadIcon").addClass("hidden");
	$(".submitButton").removeClass("disabled");
}