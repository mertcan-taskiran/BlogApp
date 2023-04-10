$('#grid-button').on('click', function () {
    $('#list-cards').hide();
    $('#grid-cards').show();
});

$('#list-button').on('click', function () {
    $('#grid-cards').hide();
    $('#list-cards').show();
});

$(document).ready(function () {
    var selectedButton = localStorage.getItem('selectedButton') || 'grid-button';
    $("#" + selectedButton).addClass("active");

    $("#list-button").click(function () {
        $(this).addClass("active");
        $("#grid-button").removeClass("active");
        localStorage.setItem('selectedButton', 'list-button');
    });

    $("#grid-button").click(function () {
        $(this).addClass("active");
        $("#list-button").removeClass("active");
        localStorage.setItem('selectedButton', 'grid-button');
    });

    $("#" + selectedButton).click();
});