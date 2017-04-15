(function() {
	

	$.ajax({
		//url: "/book/GetAuthorIds/" + bookId,
		url: "/author/getAll",
		type: "POST",
		success: function(data) {
			var authorDropdown = $(".authorDropdown");
			var bookId = $(".dl-horizontal").data().id;
			var l = data.length;

			for (var i = 0; i < l; i++) {
				authorDropdown.append("<option value=" + data[i].Id + ">" + data[i].Shortcut + "</option>");
			}

			$.ajax({
				url: "/book/GetAuthorIds/" + bookId,
				type: "POST",
				success: function(data) {
					var authorDropdown = $(".authorDropdown");
					var l = data.length;
					var options = authorDropdown.children("option");
					var l2 = options.length;

					for (var i = 0; i < l; i++) {
						for (var j = 0; j < l2; j++) {
							if (options[j].value === data[i]) {
								$(options[j]).attr("selected", "selected");
							}
						}
					}

					$(".authorDropdown").chosen({ no_results_text: "Oops, nothing found!" });

				}
			});
		}
	});
})();