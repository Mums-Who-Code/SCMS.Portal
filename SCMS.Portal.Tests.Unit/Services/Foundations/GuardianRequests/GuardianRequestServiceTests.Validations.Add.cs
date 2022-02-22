// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianIsNullAndLogItAsync()
        {
            // given
            GuardianRequest invalidGuardianRequest = null;
            var nullGuardianRequestException = new NullGuardianRequestException();

            var expectedGuardianRequestValidationException =
                new GuardianRequestValidationException(nullGuardianRequestException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(invalidGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestValidationException>(() =>
                addGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianIsInvalidAndLogItAsync(string invalidFirstName)
        {
            // given
            GuardianRequest invalidGuardianRequest = new GuardianRequest
            {
                FirstName = invalidFirstName,
                Title = GuardianRequestTitle.None,
                ContactLevel = GetInvalidEnum<GuardianRequestContactLevel>(),
                Relationship = GetInvalidEnum<GuardianRequestRelationship>()
            };

            var invalidGuardianRequestException = new InvalidGuardianRequestException();

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.Id),
                values: "Id is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.FirstName),
                values: "Text is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.LastName),
                values: "Text is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.Title),
                values: "Value is invalid.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.ContactLevel),
                values: "Value is invalid.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.Relationship),
                values: "Value is invalid.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.Email),
                values: "Text is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.CountryCode),
                values: "Text is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.ContactNumber),
                values: "Text is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.Occupation),
                values: "Text is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.StudentId),
                values: "Id is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.CreatedDate),
                values: "Date is required.");

            invalidGuardianRequestException.AddData(
                key: nameof(GuardianRequest.CreatedBy),
                values: "Id is required.");

            var expectedGuardianRequestValidationException =
                new GuardianRequestValidationException(invalidGuardianRequestException);

            // when
            ValueTask<GuardianRequest> addGuardianRequestTask =
                this.guardianRequestService.AddGuardianRequestAsync(invalidGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestValidationException>(() =>
                addGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
