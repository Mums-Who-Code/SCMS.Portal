// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
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
            Exception criticalDependencyException)
        {
            // given
            var failedSchoolDependencyException =
                new FailedSchoolDependencyException(criticalDependencyException);

            var expectedSchoolDependencyException =
                new SchoolDependencyException(failedSchoolDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSchoolsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<IQueryable<School>> retrieveAllSchoolsTask =
                this.schoolService.RetrieveAllSchools();

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
    }
}
