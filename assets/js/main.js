document.addEventListener('DOMContentLoaded', function() {

	if (window.innerWidth <= 720) {
		document.querySelector('#sidebar').classList.add('hidden');
	}

	document.querySelector('#sidebarExpand').addEventListener('click', function() {
		document.querySelector('#sidebar').classList.remove('hidden');
	});

	document.querySelector('#sidebarCollapse').addEventListener('click', function() {
		document.querySelector('#sidebar').classList.add('hidden');
	});
});

var navBlur = new BackBlur({
	selector: "#sidebar",
	background_color: 'rgba(115, 134, 213, 0.80)'
});

var bigBoxBlur = new BackBlur({
	selector: ".bigBox",
	background_color: 'rgba(100, 100, 100, 0.65)'
});