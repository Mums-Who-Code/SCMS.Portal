﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Guardians;
using SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Guardian someGuardian = CreateRandomGuardian();

            var failedGuardianDependencyException =
                new FailedGuardianDependencyException(criticalDependencyException);

            var expectedGuardianDependencyException =
                new GuardianDependencyException(failedGuardianDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyException>(() =>
                addGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedGuardianDependencyException))),
                        Times.Once());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ApiDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfErrorOccursAndLogItAsync(
            Exception apiDependencyException)
        {
            // given
            Guardian someGuardian = CreateRandomGuardian();

            var failedGuardianDependencyException =
                new FailedGuardianDependencyException(apiDependencyException);

            var expectedGuardianDependencyException =
                new GuardianDependencyException(failedGuardianDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()))
                    .ThrowsAsync(apiDependencyException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyException>(() =>
                addGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianDependencyException))),
                        Times.Once());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
