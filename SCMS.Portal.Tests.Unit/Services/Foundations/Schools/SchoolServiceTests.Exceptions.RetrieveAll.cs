// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Models.Foundations.Schools.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAndLogItAsync(
            Exception apiDependencyException)
        {
            // given
            var failedSchoolDependencyException =
                new FailedSchoolDependencyException(apiDependencyException);

            var expectedSchoolDependencyException =
                new SchoolDependencyException(failedSchoolDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSchoolsAsync())
                    .ThrowsAsync(apiDependencyException);

            // when
            ValueTask<List<School>> retrieveAllSchoolsTask =
                this.schoolService.RetrieveAllSchoolsAsync();

            // then
            await Assert.ThrowsAsync<SchoolDependencyException>(() =>
               retrieveAllSchoolsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllSchoolsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedSchoolDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ApiDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAndLogItAsync(
            Exception apiDependencyException)
        {
            // given
            var failedSchoolDependencyException =
                new FailedSchoolDependencyException(apiDependencyException);

            var expectedSchoolDependencyException =
                new SchoolDependencyException(failedSchoolDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSchoolsAsync())
                    .ThrowsAsync(apiDependencyException);

            // when
            ValueTask<List<School>> retrieveAllSchoolsTask =
                this.schoolService.RetrieveAllSchoolsAsync();

            // then
            await Assert.ThrowsAsync<SchoolDependencyException>(() =>
               retrieveAllSchoolsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllSchoolsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedSchoolServiceException =
                new FailedSchoolServiceException(serviceException);

            var expectedSchoolServiceException =
                new SchoolServiceException(failedSchoolServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSchoolsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<School>> retrieveAllSchoolsTask =
                this.schoolService.RetrieveAllSchoolsAsync();

            // then
            await Assert.ThrowsAsync<SchoolServiceException>(() =>
               retrieveAllSchoolsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllSchoolsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolServiceException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
