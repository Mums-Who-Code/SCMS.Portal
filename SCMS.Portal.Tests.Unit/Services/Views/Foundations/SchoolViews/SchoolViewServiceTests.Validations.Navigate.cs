// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldThrowValidationExceptionOnNavigateIfRouteIsInvalidAndLogit(string invalidRoute)
        {
            // given
            var invalidSchoolViewException =
                new InvalidSchoolViewException(
                   parameterName: "Route",
                   parameterValue: invalidRoute);

            var expectedSchoolViewValidationException =
                new SchoolViewValidationException(invalidSchoolViewException);

            // when
            Action navigateToAction = () =>
                this.schoolViewService.NavigateTo(invalidRoute);

            // then
            Assert.Throws<SchoolViewValidationException>(navigateToAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolViewValidationException))),
                        Times.Once);

            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(It.IsAny<string>()),
                    Times.Never);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.schoolServiceMock.VerifyNoOtherCalls();
        }
    }
}
