/// <reference path="MyScript.js" />
var Provinces = []
//fetch categories from database

function LoadProvinces(element) {
    if (Province.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: 'GET',
            url: '/Users/getProvinces',
            success: function (data) {
                Provinces = data;
                //render category
                renderProvince(element);

            }
        })
    }
    else {
        //render category to element
        renderProvince(element);
    }
}

function renderProvince(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Seleadsadasdact'));
    $.each(Provinces, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}

//fetch product

function LoadKabupaten(ProvinceDD) {
    $.ajax({
        type: "GET",
        url: '/Users/getRegencies',
        data: { 'Id': $(ProvinceDD).val() },
        success: function (data) {
            //render products to appropriate dropdown
            renderRegencies($(ProvinceDD).parents('.mycontainer').find('select.Kabupaten'), data);

        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderRegencis(element, data) {
    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}

LoadProvinces($('#Provinsi'));