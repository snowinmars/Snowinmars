(function () {
	// toggle navbar buttons

	var parser = document.createElement("a");
	parser.href = location.href;
	var page = parser.pathname.split("/")[1];
	var navbar = $(".navbar-nav");
	navbar.children().removeClass("active");

	switch (page) {
		case "author":
		case "Author":
			navbar.children("[data-id=author]").addClass("active");
			break;
		case "book":
		case "Book":
			navbar.children("[data-id=book]").addClass("active");
			break;
	}
})();