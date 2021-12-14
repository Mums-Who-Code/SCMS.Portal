// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Processings.SchoolViews.Exceptions;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Processings.SchoolViews
{
    public partial class SchoolViewProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAndLogIt(
            Xeption dependencyException)
        {
            // given
            var expectedSchoolViewProcessingDependencyException =
                new SchoolViewProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<SchoolView>> retrieveAllSchoolViewsTask =
                this.schoolViewProcessingService.RetrieveAllSchoolViewsAsync();

            // then
            await Assert.ThrowsAsync<SchoolViewProcessingDependencyException>(() =>
                retrieveAllSchoolViewsTask.AsTask());

            this.schoolViewServiceMock.Verify(service =>
                service.RetrieveAllSchoolViewsAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolViewProcessingDependencyException))),
                        Times.Once);

            this.schoolViewServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            // given
            var serviceException = new Exception();

            var failedSchoolViewProcessingException =
                new FailedSchoolViewProcessingException(serviceException);

            var expectedSchoolViewProcessingServiceException =
                new SchoolViewProcessingServiceException(
                    failedSchoolViewProcessingException.InnerException as Xeption);

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<SchoolView>> retrieveAllSchoolViewsTask =
                this.schoolViewProcessingService.RetrieveAllSchoolViewsAsync();

            // then
            await Assert.ThrowsAsync<SchoolViewProcessingServiceException>(() =>
                retrieveAllSchoolViewsTask.AsTask());

            this.schoolViewServiceMock.Verify(service =>
                service.RetrieveAllSchoolViewsAsync(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolViewProcessingServiceException))),
                        Times.Once);

            this.schoolViewServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
