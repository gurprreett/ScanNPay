$(function () {
    // Code to run when the DOM is ready
    //alert('called');
    $('#States').on('change', function () {
        //alert('on change called');
        var id = $(this).val();
        $('#Cities').empty();
        $('#Cities').append("<option>Select City</option>");
        $.ajax({
            url: '/Home/GetCities?StateId=' + id,
            success: function (res) {
                $.each(res, function (i, data) {
                    //console.log(data);
                    $('#Cities').append("<option value='" + data.cityId + "'>" + data.cityName + "</option>");
                });
            }
        });
    });
});