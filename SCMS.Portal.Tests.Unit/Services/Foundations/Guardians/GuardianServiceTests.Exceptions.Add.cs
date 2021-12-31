// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RESTFulSense.Exceptions;
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

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestAndLogItAsync()
        {
            // given
            IDictionary randomDictionary = CreateRandomDictionary();
            IDictionary exceptionData = randomDictionary;
            string randomMessage = GetRandomMessage();
            string responseMessage = randomMessage;
            var httpResponseMessage = new HttpResponseMessage();
            Guardian someGuardian = CreateRandomGuardian();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    httpResponseMessage,
                    responseMessage);

            httpResponseBadRequestException.AddData(exceptionData);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            var invalidGuardianException =
                new InvalidGuardianException(
                    httpResponseBadRequestException,
                    exceptionData);

            var expectedGuardianDependencyError =
                new GuardianDependencyValidationException(invalidGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyValidationException>(() =>
                addGuardianTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                        expectedGuardianDependencyError))),
                            Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDepedencyValidationExceptionOnAddIfValidationErrorOccursAndLogItAsync(
            Exception dependencyValidaionException)
        {
            // given
            Guardian someGuardian = CreateRandomGuardian();

            var invalidGuardianException =
                new InvalidGuardianException(
                    dependencyValidaionException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()))
                    .ThrowsAsync(dependencyValidaionException);

            var expectedGuardianDependencyException =
                new GuardianDependencyValidationException(invalidGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyValidationException>(() =>
                addGuardianTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                        expectedGuardianDependencyException))),
                            Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();
            Guardian someGuardian = CreateRandomGuardian();

            var failedGuardianServiceException =
                new FailedGuardianServiceException(serviceException);

            var expectedGuardianServiceException =
                new GuardianServiceException(failedGuardianServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianServiceException>(() =>
                addGuardianTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianServiceException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
