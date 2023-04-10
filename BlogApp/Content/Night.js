function toggleNightMode() {
    var body = document.getElementsByTagName("body")[0];
    var buttons = document.getElementsByClassName("btn");
    var h5Elements = document.querySelectorAll(".card h5");
    var pElements = document.querySelectorAll(".card p");

    if (body.classList.contains("night-mode")) {
        body.classList.remove("night-mode");
        for (var i = 0; i < buttons.length; i++) {
            buttons[i].classList.remove("btn-light");
            buttons[i].classList.add("btn-dark");
        }
        for (var i = 0; i < h5Elements.length; i++) {
            h5Elements[i].classList.remove("text-white");
        }
        for (var i = 0; i < pElements.length; i++) {
            pElements[i].classList.remove("text-white");
        }
    } else {
        body.classList.add("night-mode");
        for (var i = 0; i < h5Elements.length; i++) {
            h5Elements[i].classList.add("text-white");
        }
        for (var i = 0; i < pElements.length; i++) {
            pElements[i].classList.add("text-white");
        }
    }
}

// icon
const themeIcon = document.getElementById("theme-icon");
// localStorage'da 'theme' anahtarının varlığı kontrol edilir.
if (localStorage.getItem("theme") === "dark") {
    toggleNightMode(); // Eğer 'dark' ise, gece modu aktifleştirilir.
    themeIcon.classList.replace("fa-sun", "fa-moon"); // Buton ikonu güncellenir.
}
// Butona tıklandığında tema değiştirilir ve localStorage'a tema kaydedilir.
themeIcon.addEventListener("click", () => {
    toggleNightMode();
    if (document.body.classList.contains("night-mode")) {
        themeIcon.classList.replace("fa-moon", "fa-sun");
        localStorage.setItem("theme", "dark");
    } else {
        themeIcon.classList.replace("fa-sun", "fa-moon");
        localStorage.setItem("theme", "light");
    }
});