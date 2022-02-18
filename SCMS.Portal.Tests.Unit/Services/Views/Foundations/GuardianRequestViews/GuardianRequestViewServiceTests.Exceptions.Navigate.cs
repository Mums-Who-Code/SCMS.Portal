// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnNavigateIfServiceErrorOccursAndLogIt()
        {
            // given
            string someRoute = GetRandomRoute();
            var serviceException = new Exception();

            var failedGuardianRequestViewServiceException =
                new FailedGuardianRequestViewServiceException(serviceException);

            var expectedguardianRequestViewServiceException =
                new GuardianRequestViewServiceException(failedGuardianRequestViewServiceException);

            this.navigationBrokerMock.Setup(service =>
                service.NavigateTo(It.IsAny<string>()))
                    .Throws(serviceException);

            // when
            Action navigateToAction = () =>
                this.guardianRequestViewService.NavigateTo(someRoute);

            // then
            Assert.Throws<GuardianRequestViewServiceException>(
                navigateToAction);

            this.navigationBrokerMock.Verify(service =>
                service.NavigateTo(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedguardianRequestViewServiceException))),
                        Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }
    }
}
