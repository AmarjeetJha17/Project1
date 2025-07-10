// Enable tooltips everywhere
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

// Form validation
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

// Dashboard charts
function renderCharts() {
    // This would be replaced with actual chart rendering code
    // For example using Chart.js
    console.log('Charts would be rendered here');
}

document.addEventListener('DOMContentLoaded', renderCharts);