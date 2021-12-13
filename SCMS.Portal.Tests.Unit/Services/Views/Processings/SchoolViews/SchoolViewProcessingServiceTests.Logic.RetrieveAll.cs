// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Processings.SchoolViews
{
    public partial class SchoolViewProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllSchoolViewsAsync()
        {
            // given
            List<SchoolView> randomSchoolViews = CreateRandomSchoolViews();
            List<SchoolView> retrievedSchoolViews = randomSchoolViews;
            List<SchoolView> expectedSchoolViews = retrievedSchoolViews.DeepClone();

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ReturnsAsync(retrievedSchoolViews);

            // when
            List<SchoolView> actualSchoolViews =
                await this.schoolViewProcessingService.RetrieveAllSchoolViewsAsync();

            // then
            actualSchoolViews.Should().BeEquivalentTo(expectedSchoolViews);

            this.schoolViewServiceMock.Verify(service =>
                service.RetrieveAllSchoolViewsAsync(),
                    Times.Once());

            this.schoolViewServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
