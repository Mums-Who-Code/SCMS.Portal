// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Schools;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllSchools()
        {
            // given
            IQueryable<School> randomSchools = CreateRandomSchools();
            IQueryable<School> retrievedSchools = randomSchools;
            IQueryable<School> expectedSchools = retrievedSchools;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSchoolsAsync())
                    .ReturnsAsync(retrievedSchools);

            // when
            IQueryable<School> actualSchools =
                await this.schoolService.RetrieveAllSchools();

            // then
            actualSchools.Should().BeEquivalentTo(expectedSchools);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllSchoolsAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
