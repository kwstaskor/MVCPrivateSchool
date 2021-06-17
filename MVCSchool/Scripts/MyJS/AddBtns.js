$("#addStd").hide();
$("#stdBtn").click(function () {

    $("#addStd").slideToggle(800);
    $(this).text($(this).text() == 'Add Student' ? 'Close' : 'Add Student');
    $(this).toggleClass("btn-danger");

});

$("#addTr").hide();
$("#trBtn").click(function () {

    $("#addTr").slideToggle(800);
    $(this).text($(this).text() == 'Add Trainer' ? 'Close' : 'Add Trainer');
    $(this).toggleClass("btn-danger");

});

$("#addAss").hide();
$("#assBtn").click(function () {

    $("#addAss").slideToggle(800);
    $(this).text($(this).text() == 'Add Assignment' ? 'Close' : 'Add Assignment');
    $(this).toggleClass("btn-danger");

});

$("#addCrs").hide();
$("#crsBtn").click(function () {

    $("#addCrs").slideToggle(800);
    $(this).text($(this).text() == 'Add Course' ? 'Close' : 'Add Course');
    $(this).toggleClass("btn-danger");

});

