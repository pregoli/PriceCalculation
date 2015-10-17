$(document).ready(function () {

    var addButterEndpoint = "/basket/AddButter";
    var removeButterEndpoint = "/basket/RemoveButter";

    var addBreadEndpoint = "/basket/AddBread";
    var removeBreadEndpoint = "/basket/RemoveBread";

    var addMilkEndpoint = "/basket/AddMilk";
    var removeMilkEndpoint = "/basket/RemoveMilk";

    ShowOffers(false, null);

    //BUTTER
    var butterData = JSON.stringify({
        butter: {
            UnitPrice: 0.8
        }
    });

    $("body").on("click", ".js-add-butter", function (e) {
        DoAjaxCall(addButterEndpoint, butterData, "POST");
    });

    $("body").on("click", ".js-remove-butter", function (e) {
        DoAjaxCall(removeButterEndpoint, butterData, "POST");
    });
    //END BUTTER

    //BREAD
    var breadData = JSON.stringify({
        bread: {
            UnitPrice: 1
        }
    });

    $("body").on("click", ".js-add-bread", function (e) {
        DoAjaxCall(addBreadEndpoint, breadData, "POST");
    });

    $("body").on("click", ".js-remove-bread", function (e) {
        DoAjaxCall(removeBreadEndpoint, breadData, "POST");
    });
    //END BREAD

    //MILK
    var milkData = JSON.stringify({
        milk: {
            UnitPrice: 1.15
        }
    });
    $("body").on("click", ".js-add-milk", function (e) {
        DoAjaxCall(addMilkEndpoint, milkData, "POST");
    });

    $("body").on("click", ".js-remove-milk", function (e) {
        DoAjaxCall(removeMilkEndpoint, milkData, "POST");
    });
    //END MILK

    $("body").on("click", ".js-do-order", function (e) {
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

    function SetPrices(finalPrice, OriginalPrice, SavedMoney) {
        $('.js-final-price').html("£" + finalPrice);
        $('.js-original-price').html("£" + OriginalPrice);
        $('.js-saved-money').html("£" + SavedMoney);
    }
});