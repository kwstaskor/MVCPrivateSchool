$("#editStd").hide();
$("#stdEBtn").click(function () {

    $("#editStd").slideToggle(800);
    $(this).text($(this).text() == 'Edit Students' ? 'Close' : 'Edit Students');
    $(this).toggleClass("btn-danger");

});

$("#editTr").hide();
$("#trEBtn").click(function () {

    $("#editTr").slideToggle(800);
    $(this).text($(this).text() == 'Edit Trainers' ? 'Close' : 'Edit Trainers');
    $(this).toggleClass("btn-danger");

});

$("#editAss").hide();
$("#assEBtn").click(function () {

    $("#editAss").slideToggle(800);
    $(this).text($(this).text() == 'Edit Assignments' ? 'Close' : 'Edit Assignments');
    $(this).toggleClass("btn-danger");

});

$("#editCrs").hide();
$("#crsEBtn").click(function () {

    $("#editCrs").slideToggle(800);
    $(this).text($(this).text() == 'Edit Courses' ? 'Close' : 'Edit Courses');
    $(this).toggleClass("btn-danger");

});