// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldThrowValidationExceptionOnNavigateIfRouteIsInvalidAndLogitAsync(
            string invalidRoute)
        {
            // given
            var invalidGuardianRequestViewException =
               new InvalidGuardianRequestViewException(
                   parameterName: "Route",
                   parameterValue: invalidRoute);

            var expectedGuardianRequestViewValidationException =
                new GuardianRequestViewValidationException(invalidGuardianRequestViewException);

            // when
            Action navigateToAction = () =>
                this.guardianRequestViewService.NavigateTo(invalidRoute);

            // then
            Assert.Throws<GuardianRequestViewValidationException>(navigateToAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestViewValidationException))),
                        Times.Once);

            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(It.IsAny<string>()),
                    Times.Never);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }
    }
}
