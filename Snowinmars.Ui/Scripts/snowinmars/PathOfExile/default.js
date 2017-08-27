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

	$(".textareaCopyBtn").on("click",
		function () {
			$(".importExportTextarea").select();
			document.execCommand("copy");
		});
})();