var SnowinmarsGlobal = SnowinmarsGlobal || {};

SnowinmarsGlobal.namespace = function (namespaceString) {
	var parts = namespaceString.split("."),
		parent = SnowinmarsGlobal,
		i;

	if (parts[0] === "SnowinmarsGlobal") {
		parts = parts.slice(1);
	}

	for (i = 0; i < parts.length; i += 1) {
		if (typeof parent[parts[i]] === "undefined") {
			parent[parts[i]] = {};
		}

		parent = parent[parts[i]];
	}

	return parent;
};







(function () {
	$(".clearQualities").on("click",
		function () {
			$.each($(".quality"),
				function (index, value) {
					value.value = "";
				});
		});

	$(".clearResultSetBtn").on("click",
		function () {
			$(".result").empty();
		});
})();