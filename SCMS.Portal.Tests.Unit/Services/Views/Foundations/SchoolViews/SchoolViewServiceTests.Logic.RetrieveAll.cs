// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllSchoolViewsAsync()
        {
            // given
            List<dynamic> randomSchoolViewPropertiesCollection =
                CreatedRandomSchoolViewCollections();

            List<School> randomSchools =
                randomSchoolViewPropertiesCollection.Select(property =>
                    new School
                    {
                        Id = property.Id,
                        Name = property.Name,
                        CreatedDate = property.CreatedDate,
                        UpdatedDate = property.UpdatedDate,
                        CreatedBy = property.CreatedBy,
                        UpdatedBy = property.UpdatedBy
                    }).ToList();

            List<School> retrievedSchools = randomSchools;

            List<SchoolView> randomSchoolViews =
                randomSchoolViewPropertiesCollection.Select(property =>
                    new SchoolView
                    {
                        Id = property.Id,
                        Name = property.Name
                    }).ToList();

            List<SchoolView> expectedSchoolViews = randomSchoolViews;

            this.schoolServiceMock.Setup(service =>
                service.RetrieveAllSchoolsAsync())
                    .ReturnsAsync(retrievedSchools);

            // when
            List<SchoolView> actualSchoolViews =
                await this.schoolViewService.RetrieveAllSchoolViewsAsync();

            // then
            actualSchoolViews.Should().BeEquivalentTo(expectedSchoolViews);

            this.schoolServiceMock.Verify(service =>
                service.RetrieveAllSchoolsAsync(),
                    Times.Once());

            this.schoolServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
