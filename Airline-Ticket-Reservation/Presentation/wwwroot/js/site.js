// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let previousSelectedSeat = null;

document.getElementById('seatContainer').addEventListener('click', function (event) {
    
    // Check if the clicked element has the "available" class
    if (event.target.classList.contains('available')) {
        let seatId = event.target.getAttribute('data-seat-id');

        document.getElementById("seatId").value = seatId;
        document.getElementById("seatName").value = event.target.getAttribute('data-seat-name');
        
        if (previousSelectedSeat != null) {
            previousSelectedSeat.classList.remove('selected');
        }
        
        event.target.classList.add('selected');
        previousSelectedSeat = event.target;
    }
});