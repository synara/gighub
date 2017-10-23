var FollowService = function () {

    var FollowingArtists = function (followeeId, done, fail) {
        $.post('/api/followings', { followeeId: followeeId })
       .done(done)
       .fail(fail);
    };

    var UnfollowArtists = function (followeeId, done, fail) {
        $.ajax({
            url: '/api/followings/' + followeeId,
            method: "DELETE"
        })
         .done(done)
         .fail(fail);

    };

    return {
        UnfollowArtist: UnfollowArtists,
        FollowingArtist: FollowingArtists
    }

}();

