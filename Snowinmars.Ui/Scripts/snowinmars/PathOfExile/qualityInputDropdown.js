(function () {
	var renderEngine = SnowinmarsGlobal.namespace("SnowinmarsGlobal.renderEngine");

	$(".toSelect").each(renderEngine.convertToSelect);
	$(".toInput").each(renderEngine.convertToInput);
})();