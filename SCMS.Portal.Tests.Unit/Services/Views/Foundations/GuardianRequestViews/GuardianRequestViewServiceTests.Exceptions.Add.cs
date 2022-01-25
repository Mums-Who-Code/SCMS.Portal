// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {

        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfValidationErrorOccuredAndLogItAsync(
            Exception guardianRequestServiceValidationException)
        {
            //given
            GuardianRequestView someGuardianRequestView = CreateRandomGuardianRequestView();

            var expectedDependencyValidationException =
                new GuardianRequestViewDependencyValidationException(guardianRequestServiceValidationException.InnerException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(guardianRequestServiceValidationException);

            //when
            ValueTask<GuardianRequestView> addGuardianRequestViewTask =
                this.guardianRequestViewService.AddGuardianRequestViewAsync(someGuardianRequestView);

            //then
            await Assert.ThrowsAsync<GuardianRequestViewDependencyValidationException>(() =>
                addGuardianRequestViewTask.AsTask());

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccuredAndLogItAsync(
            Exception guardianRequestServiceDependencyException)
        {
            //given
            GuardianRequestView someGuardianRequestView = CreateRandomGuardianRequestView();

            var expectedDependencyException =
                new GuardianRequestViewDependencyException(guardianRequestServiceDependencyException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(guardianRequestServiceDependencyException);

            //when
            ValueTask<GuardianRequestView> addGuardianRequestViewTask =
                this.guardianRequestViewService.AddGuardianRequestViewAsync(someGuardianRequestView);

            //then
            await Assert.ThrowsAsync<GuardianRequestViewDependencyException>(() =>
                addGuardianRequestViewTask.AsTask());

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }

    }
}
