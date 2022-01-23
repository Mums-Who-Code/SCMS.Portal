// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var failedGuardianRequestDependencyException =
                new FailedGuardianRequestDependencyException(criticalDependencyException);

            var expectedGuardianRequestDependencyException =
                new GuardianRequestDependencyException(failedGuardianRequestDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestDependencyException>(() =>
                addGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedGuardianRequestDependencyException))),
                        Times.Once());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
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
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var failedGuardianRequestDependencyException =
                new FailedGuardianRequestDependencyException(apiDependencyException);

            var expectedGuardianRequestDependencyException =
                new GuardianRequestDependencyException(
                    failedGuardianRequestDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()))
                    .ThrowsAsync(apiDependencyException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestDependencyException>(() =>
                addGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestDependencyException))),
                        Times.Once());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
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
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    httpResponseMessage,
                    responseMessage);

            httpResponseBadRequestException.AddData(exceptionData);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            var invalidGuardianRequestException =
                new InvalidGuardianRequestException(
                    httpResponseBadRequestException,
                    exceptionData);

            var expectedGuardianRequestDependencyError =
                new GuardianRequestDependencyValidationException(
                    invalidGuardianRequestException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestDependencyValidationException>(() =>
                addGuardianRequestTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                        expectedGuardianRequestDependencyError))),
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
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var invalidGuardianRequestException =
                new InvalidGuardianRequestException(
                    dependencyValidaionException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()))
                    .ThrowsAsync(dependencyValidaionException);

            var expectedGuardianRequestDependencyException =
                new GuardianRequestDependencyValidationException(
                    invalidGuardianRequestException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(
                    someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestDependencyValidationException>(() =>
                addGuardianRequestTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                        expectedGuardianRequestDependencyException))),
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
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var failedGuardianRequestServiceException =
                new FailedGuardianRequestServiceException(serviceException);

            var expectedGuardianRequestServiceException =
                new GuardianRequestServiceException(
                    failedGuardianRequestServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestServiceException>(() =>
                addGuardianRequestTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestServiceException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
