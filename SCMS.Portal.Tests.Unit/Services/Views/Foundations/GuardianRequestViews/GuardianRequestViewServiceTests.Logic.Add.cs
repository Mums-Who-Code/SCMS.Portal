// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        [Fact]
        public async Task ShouldAddGuardianRequestViewAsync()
        {
            // given
            Guid userId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();

            dynamic randomGuardianRequestViewProperties =
                CreateRandomGuardianRequestViewProperties(
                    userId,
                    randomDateTime);

            var randomGuardianRequestView = new GuardianRequestView
            {
                Title = (GuardianRequestViewTitle)randomGuardianRequestViewProperties.Title,
                FirstName = randomGuardianRequestViewProperties.FirstName,
                LastName = randomGuardianRequestViewProperties.LastName,
                Email = randomGuardianRequestViewProperties.Email,
                CountryCode = randomGuardianRequestViewProperties.CountryCode,
                ContactNumber = randomGuardianRequestViewProperties.ContactNumber,
                Occupation = randomGuardianRequestViewProperties.Occupation,
                ContactLevel = (GuardianRequestViewContactLevel)randomGuardianRequestViewProperties.ContactLevel,
                Relationship = (GuardianRequestViewRelationship)randomGuardianRequestViewProperties.Relationship,
                StudentId = randomGuardianRequestViewProperties.StudentId
            };

            var inputGuardianRequestView = randomGuardianRequestView;
            var expectedGuardianRequestView = inputGuardianRequestView.DeepClone();

            var randomGuardianRequestStudent = new GuardianRequest
            {
                Id = randomGuardianRequestViewProperties.Id,
                Title = (GuardianRequestTitle)randomGuardianRequestViewProperties.Title,
                FirstName = randomGuardianRequestViewProperties.FirstName,
                LastName = randomGuardianRequestViewProperties.LastName,
                Email = randomGuardianRequestViewProperties.Email,
                CountryCode = randomGuardianRequestViewProperties.CountryCode,
                ContactNumber = randomGuardianRequestViewProperties.ContactNumber,
                Occupation = randomGuardianRequestViewProperties.Occupation,
                ContactLevel = (GuardianRequestContactLevel)randomGuardianRequestViewProperties.ContactLevel,
                Relationship = (GuardianRequestRelationship)randomGuardianRequestViewProperties.Relationship,
                StudentId = randomGuardianRequestViewProperties.StudentId,
                CreatedDate = randomGuardianRequestViewProperties.CreatedDate,
                UpdatedDate = randomGuardianRequestViewProperties.UpdatedDate,
                CreatedBy = randomGuardianRequestViewProperties.CreatedBy,
                UpdatedBy = randomGuardianRequestViewProperties.UpdatedBy,
            };

            GuardianRequest expectedInputGuardianRequest = randomGuardianRequestStudent;
            GuardianRequest persistedGuardianRequest = expectedInputGuardianRequest.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            this.userServiceMock.Setup(service =>
                service.GetCurrentlyLoggedInUser())
                    .Returns(userId);

            this.guardianRequestServiceMock.Setup(service =>
                service.AddGuardianRequestAsync(It.Is(
                    SameGuardianRequestAs(expectedInputGuardianRequest))))
                        .ReturnsAsync(persistedGuardianRequest);

            // when
            GuardianRequestView actualGuardianRequestView =
                await this.guardianRequestViewService
                    .AddGuardianRequestViewAsync(inputGuardianRequestView);

            // then
            actualGuardianRequestView.Should().BeEquivalentTo(expectedGuardianRequestView);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);


            this.guardianRequestServiceMock.Verify(service =>
                service.AddGuardianRequestAsync(It.Is(
                    SameGuardianRequestAs(expectedInputGuardianRequest))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
