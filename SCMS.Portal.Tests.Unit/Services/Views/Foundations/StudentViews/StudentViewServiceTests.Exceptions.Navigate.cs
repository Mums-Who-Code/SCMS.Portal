// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnNavigateIfServiceErrorOccursAndLogIt()
        {
            //given
            string someRoute = GetRandomRoute();
            var serviceException = new Exception();

            var failedStudentViewServiceException =
                new FailedStudentViewServiceException(serviceException);

            var studentViewServiceException =
                new StudentViewServiceException(failedStudentViewServiceException);

            this.navigationBrokerMock.Setup(service =>
                service.NavigateTo(It.IsAny<string>()))
                    .Throws(serviceException);

            //when
            Action navigateToAction = () =>
                this.studentViewService.NavigateTo(someRoute);

            //then
            Assert.Throws<StudentViewServiceException>(
                navigateToAction);

            this.navigationBrokerMock.Verify(service =>
                service.NavigateTo(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    studentViewServiceException))),
                        Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }

    }
}
