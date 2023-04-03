
$(document).ready(function () {



    $(document).on("click", ".show-product-modal", function (e) {
        e.preventDefault();

        console.log(e.target)

        var url = e.target.parentElement.href;

        console.log(url)

        fetch(url)
            .then(response => response.text())
            .then(data => {
                $('.product-details-modal').html(data);
                console.log(data)
            })

        $("#quickModal").show("modal");
    })


    $(".btn-close").click(function () {
        $(".quick-view-modal").hide();
    });


    //$('.a-tag li a').filter(function () {
    //    return this.href === location.href;
    //}).addClass('active');


    /////////////////////////////

    let btns = document.querySelectorAll(".add-product-to-basket-btn")

    btns.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        console.log(e.target.href)

        var url = e.target.href
        $.ajax({
            type: "POST",
            url: url,
            success: function (response) {
                console.log(response)
                $('.mini-cart').html(response);

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);
            }
        });

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.mini-cart').html(data);
            })
    }))

    //$(document).on("click", ".add-product-to-basket-modal", function (e) {
    //    e.preventDefault();

    //    fetch(e.target.href)
    //        .then(response => response.text())
    //        .then(data => {
    //            $('.mini-cart').html(data);
    //        })
    //})



    $(document).on("click", ".remove-product-to-basket-btn", function (e) {
        e.preventDefault();
        console.log(e.target.parentElement.href)
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.mini-cart').html(data);
            })
    })



    ///////////////////////

    $(document).on("click", ".plus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.cart-item').html(data);
                    })
            })
    })






    $(document).on("click", ".minus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.cart-item').html(data);

                    })
            })
    })


});



//$(document).on("click", '.select-catagory', function (e) {
//    e.preventDefault();
//    let aHref = e.target.href;
//    let category = e.target.previousElementSibling
//    let CategoryId = category.value;


//    console.log(CategoryId)

//    console.log(aHref)



//    $.ajax(
//        {
//            type: "GET",
//            url: aHref,

//            data: {
//                CategoryId: CategoryId
//            },

//            success: function (response) {
//                console.log(response)
//                $('.filtered-area').html(response);

//            },
//            error: function (err) {
//                $(".product-details-modal").html(err.responseText);

//            }

//        });

//})

//$(document).on("click", '.select-color', function (e) {
//    e.preventDefault();
//    let aHref = e.target.href;
//    let category = e.target.previousElementSibling
//    let CategoryId = category.value;


//    console.log(CategoryId)

//    console.log(aHref)



//    $.ajax(
//        {
//            type: "GET",
//            url: aHref,

//            data: {
//                CategoryId: CategoryId
//            },

//            success: function (response) {
//                console.log(response)
//                $('.filtered-area').html(response);

//            },
//            error: function (err) {
//                $(".product-details-modal").html(err.responseText);

//            }

//        });

//})

//$(document).on("click", '.select-tag', function (e) {
//    e.preventDefault();
//    let aHref = e.target.href;
//    let category = e.target.previousElementSibling
//    let CategoryId = category.value;


//    console.log(CategoryId)

//    console.log(aHref)



//    $.ajax(
//        {
//            type: "GET",
//            url: aHref,

//            data: {
//                CategoryId: CategoryId
//            },

//            success: function (response) {
//                console.log(response)
//                $('.filtered-area').html(response);

//            },
//            error: function (err) {
//                $(".product-details-modal").html(err.responseText);

//            }

//        });

//})


//$(document).on("change", ".searchproductPrice", function (e) {
//    e.preventDefault();

//    let minPrice = e.target.previousElementSibling.children[0].children[3].innerText.slice(1);
//    let MinPrice = parseInt(minPrice);

//    let maxPrice = e.target.previousElementSibling.children[0].children[4].innerText.slice(1);
//    let MaxPrice = parseInt(maxPrice);
//    let aHref = document.querySelector(".shoppage-url").href;

//    console.log(MinPrice);
//    console.log(MaxPrice);
//    console.log(aHref)

//    $.ajax(
//        {
//            url: aHref,

//            data: {
//                MinPrice: MinPrice,
//                MaxPrice: MaxPrice

//            },

//            success: function (response) {
//                $('.filtered-area').html(response);


//            },
//            error: function (err) {
//                $(".product-details-modal").html(err.responseText);

//            }

//        });


//})