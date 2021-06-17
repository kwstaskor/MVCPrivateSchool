jQuery.validator.addMethod("datemustbeequalorgreaterthanstartdate", function (value, element, param) {
    var endDate = $("#EndDate").val();
    var startDate = $("#StartDate").val();

    if (endDate < startDate) {
        return false;
    }
    return true;
});

jQuery.validator.unobtrusive.adapters.addBool("datemustbeequalorgreaterthanstartdate");