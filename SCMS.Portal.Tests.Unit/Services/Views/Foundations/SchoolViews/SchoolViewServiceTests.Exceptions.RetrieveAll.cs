// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            var expectedSchoolViewDependencyException =
                new SchoolViewDependencyException(
                    dependencyException);

            this.schoolServiceMock.Setup(service =>
                service.RetrieveAllSchoolsAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<SchoolView>> retrieveAllSchoolViewsTask =
                this.schoolViewService.RetrieveAllSchoolsAsync();

            // then
            await Assert.ThrowsAsync<SchoolViewDependencyException>(() =>
                retrieveAllSchoolViewsTask.AsTask());

            this.schoolServiceMock.Verify(service =>
                service.RetrieveAllSchoolsAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolViewDependencyException))),
                        Times.Once);

            this.schoolServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedSchoolViewServiceException =
                new FailedSchoolViewServiceException(
                    serviceException);

            var expectedSchoolViewServiceException =
                new SchoolViewServiceException(
                    failedSchoolViewServiceException);

            this.schoolServiceMock.Setup(service =>
                service.RetrieveAllSchoolsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<SchoolView>> retrieveAllSchoolViewsTask =
                this.schoolViewService.RetrieveAllSchoolsAsync();

            // then
            await Assert.ThrowsAsync<SchoolViewServiceException>(() =>
                retrieveAllSchoolViewsTask.AsTask());

            this.schoolServiceMock.Verify(service =>
                service.RetrieveAllSchoolsAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolViewServiceException))),
                        Times.Once);

            this.schoolServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
