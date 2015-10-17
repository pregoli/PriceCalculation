$(document).ready(function () {

    //butter MVC controller actions endpoints
    var addButterEndpoint = "/basket/AddButter";
    var removeButterEndpoint = "/basket/RemoveButter";

    //bread MVC controller actions endpoints
    var addBreadEndpoint = "/basket/AddBread";
    var removeBreadEndpoint = "/basket/RemoveBread";

    //milk MVC controller actions endpoints
    var addMilkEndpoint = "/basket/AddMilk";
    var removeMilkEndpoint = "/basket/RemoveMilk";

    ShowOffers(false, null);

    //BUTTER
    //Json butter object
    var butterData = JSON.stringify({
        butter: {
            //this val is just a placeholder.Real value server side
            UnitPrice: 0.8
        }
    });

    //Add butter to basket
    $("body").on("click", ".js-add-butter", function (e) {
        DoAjaxCall(addButterEndpoint, butterData, "POST");
    });

    //Remove butter from basket
    $("body").on("click", ".js-remove-butter", function (e) {
        DoAjaxCall(removeButterEndpoint, butterData, "POST");
    });
    //END BUTTER

    //BREAD
    //Json Bread Object
    var breadData = JSON.stringify({
        bread: {
            //this val is just a placeholder.Real value server side
            UnitPrice: 1
        }
    });

    //Add bread to basket
    $("body").on("click", ".js-add-bread", function (e) {
        DoAjaxCall(addBreadEndpoint, breadData, "POST");
    });

    //Remove bread from basket
    $("body").on("click", ".js-remove-bread", function (e) {
        DoAjaxCall(removeBreadEndpoint, breadData, "POST");
    });
    //END BREAD

    //MILK
    //json Milk object
    var milkData = JSON.stringify({
        milk: {
            //this val is just a placeholder.Real value server side
            UnitPrice: 1.15
        }
    });

    //Add milk to basket
    $("body").on("click", ".js-add-milk", function (e) {
        DoAjaxCall(addMilkEndpoint, milkData, "POST");
    });

    //Add milk from basket
    $("body").on("click", ".js-remove-milk", function (e) {
        DoAjaxCall(removeMilkEndpoint, milkData, "POST");
    });
    //END MILK

    //Order form operations buttons clicks
    $("body").on("click", ".js-process-order", function (e) {
        var requestUrl = "/basket/CalculateTotalAndGetOffers";

        DoAjaxCall(requestUrl, butterData, "GET")
    });

    $("body").on("click", ".js-clear-order", function (e) {
        var requestUrl = "/basket/ClearOrder";

        DoAjaxCall(requestUrl, "", "GET");

        $("input[name='txtButterNumber']").val(0);
        $("input[name='txtBreadNumber']").val(0);
        $("input[name='txtMilkNumber']").val(0);

        SetPrices(0, 0, 0);
        ShowOffers(false, null);
    });
    //End Order form operations buttons clicks

    //Ajax call to MVC controllers
    function DoAjaxCall(requestUrl, data, type) {
        $.ajax({
            type: type,
            url: requestUrl,
            contentType: "json",
            dataType: "json",
            data: data,
            success: function (result) {
                if (result.hasOwnProperty("FinalPrice")) {

                    if (result.Offers != undefined) {
                        ShowOffers(result.Offers.length > 0, result.Offers);
                    }

                    SetPrices(result.FinalPrice, result.OriginalPrice, result.SavedAmount);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowOffers(false, null);
            }
        });
    }
    //End Ajax call to MVC controllers

    //Show or Hide the Offers boxes from the Order summary box
    function ShowOffers(show, offers)
    {
        if (show) {
            $(".js-content-offers").show();
            var offersHtmlContent = "";
            offers.forEach(function (offer) {
                offersHtmlContent += "<strong>Well done!</strong> " + offer.Message + "<hr />";
            });

            $(".js-content-offers").html(offersHtmlContent);
        }
        else {
            $(".js-content-offers").hide();
        }
    }

    //Set final prices in the order box summary
    function SetPrices(finalPrice, OriginalPrice, SavedMoney) {
        $('.js-final-price').html("£" + finalPrice);
        $('.js-original-price').html("£" + OriginalPrice);
        $('.js-saved-money').html("£" + SavedMoney);
    }
});