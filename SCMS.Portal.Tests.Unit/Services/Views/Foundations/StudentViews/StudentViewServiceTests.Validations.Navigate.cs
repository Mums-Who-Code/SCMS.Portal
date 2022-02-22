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
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldThrowValidationExceptionOnNavigateIfRouteIsInvalidAndLogitAsync(
            string invalidRoute)
        {
            //given
            var invalidStudentViewException =
               new InvalidStudentViewException(
                   parameterName: "Route",
                   parameterValue: invalidRoute);

            var expectedStudentViewValidationException =
                new StudentViewValidationException(invalidStudentViewException);

            //when
            Action navigateToAction = () =>
                this.studentViewService.NavigateTo(invalidRoute);

            //then
            Assert.Throws<StudentViewValidationException>(navigateToAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(It.IsAny<string>()),
                    Times.Never);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
