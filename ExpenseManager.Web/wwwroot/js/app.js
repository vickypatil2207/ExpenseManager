﻿function initialize() {
    document.getElementById('menu-toggle').addEventListener('click', function () {
        const sidebar = document.getElementById('sidebar');
        sidebar.classList.toggle('active');
    });
}