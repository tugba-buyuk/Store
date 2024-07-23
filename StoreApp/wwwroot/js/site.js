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
document.addEventListener('DOMContentLoaded', function () {
    const thumbnails = document.querySelectorAll('.thumbnail-img');
    const mainImage = document.getElementById('mainImage');

    thumbnails.forEach(thumbnail => {
        thumbnail.addEventListener('click', function () {
            const newSrc = thumbnail.getAttribute('src');
            mainImage.setAttribute('src', newSrc);
        });
    });
});