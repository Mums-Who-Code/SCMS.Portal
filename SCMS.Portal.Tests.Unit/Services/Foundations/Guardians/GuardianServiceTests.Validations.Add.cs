// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using NuGet.Frameworks;
using SCMS.Portal.Web.Models.Foundations.Guardians;
using SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianIsNullAndLogItAsync()
        {
            //given
            Guardian invalidGuardian = null;
            var nullGuardianException = new NullGuardianException();

            var expectedGuardianValidationException =
                new GuardianValidationException(nullGuardianException);

            //when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            //then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
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
            //given
            Guardian invalidGuardian = new Guardian
            {
                FirstName = invalidFirstName,
                Title = Title.None
            };

            var invalidGuardianException = new InvalidGuardianException();

            invalidGuardianException.AddData(
                key: nameof(Guardian.Id),
                values: "Id is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.FirstName),
                values: "Text is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.LastName),
                values: "Text is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.Title),
                values: "Value is invalid");

            invalidGuardianException.AddData(
                key: nameof(Guardian.EmailId),
                values: "Text is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.CountryCode),
                values: "Text is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.ContactNumber),
                values: "Text is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.Occupation),
                values: "Text is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.StudentId),
                values: "Id is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.CreatedDate),
                values: "Date is required");

            invalidGuardianException.AddData(
                key: nameof(Guardian.CreatedBy),
                values: "Id is required");

            var expectedGuardianValidationException =
                new GuardianValidationException(invalidGuardianException);

            //when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            //then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
