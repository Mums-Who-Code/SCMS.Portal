// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianRequestViewIsNullAndLogItAsync()
        {
            //given
            GuardianRequestView nullGuardianRequestView = null;
            var nullGuardianRequestException = new NullGuardianRequestViewException();

            var expectedGuardianRequestViewValidationException =
                new GuardianRequestViewValidationException(nullGuardianRequestException);

            //when
            ValueTask<GuardianRequestView> addGuardianRequestViewTask =
                this.guardianRequestViewService.AddGuardianRequestViewAsync(nullGuardianRequestView);

            //then
            await Assert.ThrowsAsync<GuardianRequestViewValidationException>(() =>
                addGuardianRequestViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestViewValidationException))),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.guardianRequestServiceMock.Verify(service =>
                service.AddGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }
    }
}
