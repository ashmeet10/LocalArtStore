$(document).ready(function () {
    /*Without using Jquery
     * var x = "";
    console.log("hello PluralSight");
    var theForm = document.getElementById("theForm");
    theForm.hidden = true;
    
    
    var button = document.getElementById("buyButton");
    button.addEventListener("click", function () {
        alert("Buying Item");
    });
    
    var productInfo = document.getElementsByClassName("product-props");
    //var listItems = productInfo.item[0].children;
    console.log(listItems);
    */

    var theForm = $("#theForm");
    theForm.hide();


    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());
    }
    );

    var $loginToggle = $("#loginToggel");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.fadeToggle(1000);
    });
});