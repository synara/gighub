using FluentAssertions;
using GigHub.Tests.Extensions;
using GigHubOk.Controllers.Api;
using GigHubOk.Models;
using GigHubOk.Persistence;
using GigHubOk.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);
            userId = "1";
            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser(userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void Cancel_GigsIsCancel_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendee(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigCanceledByAnotherUser_ShouldReturnNotAuthorized()
        {
            var gig = new Gig
            {
                ArtistId = userId + "-"
            };

            _mockRepository.Setup(r => r.GetGigWithAttendee(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        //[TestMethod]
        //public void Cancel_ValidRequest_ShouldReturnOk()
        //{
        //    var gig = new Gig
        //    {
        //        ArtistId = userId
        //    };

        //    _mockRepository.Setup(r => r.GetGigWithAttendee(1)).Returns(gig);
        //    var result = _controller.Cancel(1);
        //    result.Should().BeOfType<OkResult>();
        //}




    }
}

