using FluentAssertions;
using GigHub.Tests.Extensions;
using GigHubOk.Controllers.Api;
using GigHubOk.DTOs;
using GigHubOk.Models;
using GigHubOk.Persistence;
using GigHubOk.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendanceControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string userId;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();

            mockUoW.SetupGet(u => u.Attedances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            userId = "1";
            _controller.MockCurrentUser(userId, "user1@domain.com");

        }

        //[TestMethod]
        //public void Atend_GigThatHasAlreadyBeenServiced_ShouldBeBadRequest()
        //{
        //    var att = new Attendance();

        //    var dto = new AttendanceDtos
        //    {
        //        GigId = 1
        //    };

        //    var result = _controller.Atend(dto);

        //    _mockRepository.Setup(u => u.GetAttendance(1, userId)).Returns(att);


        //    result.Should().BeOfType<BadRequestErrorMessageResult>();
        //}

        [TestMethod]
        public void Atend_ValidRequest_ShoulReturnOk()
        {
            var dto = new AttendanceDtos
            {
                GigId = 1
            };

            var result = _controller.Atend(dto);
            result.Should().BeOfType<OkResult>();
        }

        //[TestMethod]
        //public void Delete_GigDeleteByAnotherUser_ReturnUnaurothorized()
        //{
        //    var att = new Attendance
        //    {
        //        AttendeeId = userId + "-"
        //    };


        //    _mockRepository.Setup(g => g.GetAttendance(1, userId)).Returns(att);
        //    var result = _controller.Delete(1);
        //    result.Should().BeOfType<UnauthorizedResult>();
        //}

        //[TestMethod]
        //public void Delete_GigDeleteValidRequest_ReturnOk()
        //{
        //    var att = new Attendance
        //    {
        //        AttendeeId = userId
        //    };

        //    _mockRepository.Setup(g => g.GetAttendance(1, userId)).Returns(att);
        //    var result = _controller.Delete(1);
        //    result.Should().BeOfType<OkResult>();
        //}
    }
}
