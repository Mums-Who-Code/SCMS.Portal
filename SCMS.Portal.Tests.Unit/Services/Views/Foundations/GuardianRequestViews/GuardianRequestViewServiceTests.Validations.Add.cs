// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianRequestViewIsNullAndLogItAsync()
        {
            //given
            GuardianRequestView nullGuardianRequestView = null;
            var nullGuardianRequestException = new NullGuardianRequestViewException();

            var expectedGuardianRequestViewValidationException =
                new GuardianRequestViewValidationException(nullGuardianRequestException);

            //when
            ValueTask<GuardianRequestView> addGuardianRequestViewTask =
                this.guardianRequestViewService.AddGuardianRequestViewAsync(nullGuardianRequestView);

            //then
            await Assert.ThrowsAsync<GuardianRequestViewValidationException>(() =>
                addGuardianRequestViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestViewValidationException))),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.guardianRequestServiceMock.Verify(service =>
                service.AddGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGardianRequestViewIsInvalidAndLogItAsync(
           string invalidText)
        {
            GuardianRequestView invalidGuardianRequestView = new GuardianRequestView
            {
                Title = GuardianRequestViewTitle.None,
                FirstName = invalidText,
                LastName = invalidText,
                EmailId = invalidText,
                CountryCode = invalidText,
                ContactNumber = invalidText,
                Occupation = invalidText,
            };

            var invalidGuardianRequestViewException = new InvalidGuardianRequestViewException();

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.Title),
                values: "Value is invalid.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.FirstName),
                values: "Text is required.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.LastName),
                values: "Text is required.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.EmailId),
                values: "Text is required.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.CountryCode),
                values: "Text is required.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.ContactNumber),
                values: "Text is required.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.Occupation),
                values: "Text is required.");

            invalidGuardianRequestViewException.AddData(
                key: nameof(GuardianRequestView.StudentId),
                values: "Id is required.");

            var expectedGuardianRequestViewValidationException =
                new GuardianRequestViewValidationException(invalidGuardianRequestViewException);

            //when
            ValueTask<GuardianRequestView> addGuardianRequestViewTask =
                this.guardianRequestViewService.AddGuardianRequestViewAsync(invalidGuardianRequestView);

            //then
            await Assert.ThrowsAsync<GuardianRequestViewValidationException>(() =>
                addGuardianRequestViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestViewValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.guardianRequestServiceMock.Verify(service =>
                service.AddGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
        }
    }
}
