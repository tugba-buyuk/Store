$(function () {
    // Owl Carousel
    var owl = $(".owl-carousel");
    owl.owlCarousel({
        items: 1
        ,
        margin: 10,
        loop: true,
        nav: true
    });
});
const thumbnails = document.querySelectorAll('.thumbnail-img');
console.log("Thumbnail:", thumbnails);
// Get the main image element
const mainImage = document.querySelector('.main-img');
console.log("MAINIMAGE:", mainImage);
// Add click event listener to each thumbnail
thumbnails.forEach(thumb => {
    thumb.addEventListener('click', (event) => {
        // Change the src attribute of the main image
        console.log("Event:", event);
        mainImage.src = event.target.dataset.image;
    });
});