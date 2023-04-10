var searchIcon = document.getElementById('search-icon');
var searchOverlay = document.getElementById('search-overlay');

searchIcon.addEventListener('click', function () {
    searchOverlay.style.display = 'flex';
});

searchOverlay.addEventListener('click', function (event) {
    if (event.target === searchOverlay) {
        searchOverlay.style.display = 'none';
    }
});