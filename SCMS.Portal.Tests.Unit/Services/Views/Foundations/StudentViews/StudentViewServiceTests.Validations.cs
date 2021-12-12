// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentViewIsNullAndLogItAsync()
        {
            //given
            StudentView nullStudentView = null;
            var nullStudentException = new NullStudentViewException();

            var expectedStudentViewValidationException =
                new StudentViewValidationException(nullStudentException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(nullStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewValidationException>(() =>
                addStudentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentViewIsInvalidAndLogItAsync(
            string invalidText)
        {
            //given
            StudentView invalidStudentView = new StudentView
            {
                FirstName = invalidText,
                LastName = invalidText,
                DateOfBirth = default,
                Gender = GetValidEnum<StudentGenderView>()
            };

            var invalidStudentViewException = new InvalidStudentViewException();

            invalidStudentViewException.AddData(
                key: nameof(StudentView.FirstName),
                values: "Text is required.");

            invalidStudentViewException.AddData(
                key: nameof(StudentView.LastName),
                values: "Text is required.");

            invalidStudentViewException.AddData(
                key: nameof(StudentView.DateOfBirth),
                values: "Date is required.");

            invalidStudentViewException.AddData(
                key: nameof(StudentView.SchoolId),
                values: "Id is required.");

            var expectedStudentViewValidationException =
                new StudentViewValidationException(invalidStudentViewException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(invalidStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewValidationException>(() =>
                addStudentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
