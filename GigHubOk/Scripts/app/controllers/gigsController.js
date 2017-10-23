var GigsController = function (attendanceService) {
    var $button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendaces);

    };

    var toggleAttendaces = function (e) {
        $button = $(e.target);

        var gigId = $button.attr("data-gigId");

        if ($button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);

    };

    var done = function () {
        var text = ($button.text() == "Going") ? "Going?" : "Going";

        $button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something fail!!");
    };


    return {
        init: init
    }

}(AttendanceService);
