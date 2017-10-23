var FollowController = function (followService) {

    var $follow;

    var init = function () {
        $(".js-toggle-follow").click(toggles);
    };

    var toggles = function (e) {
        $follow = $(e.target);

        var followeeId = $follow.attr("artistId");

        if ($follow.hasClass("btn-default"))
            followService.FollowingArtist(followeeId, done, fail);
        else
            followService.UnfollowArtist(followeeId, done, fail);
    };

    var done = function () {
        var text = ($follow.text() == "Follow") ? "Following" : "Follow";
        $follow.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something fail!!");
    };

    return {
        init: init
    }

}(FollowService);
