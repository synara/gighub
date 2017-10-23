var AttendanceService = function () {

    var createAttendances = function (gigId, done, fail) {
        $.post('/api/Attendances', { "gigId": gigId })
           .done(done)
           .fail(fail);
    };


    var deleteAttendances = function (gigId, done, fail) {
        $.ajax({
            url: "/api/Attendances/" + gigId,
            method: "DELETE"
        })
           .done(done)
           .fail(fail);
    };

    return {
        createAttendance: createAttendances,
        deleteAttendance: deleteAttendances
    }

}();
