// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentIsNullAndLogItAsync()
        {
            //given
            Student invalidStudent = null;
            var nullStudentException = new NullStudentException();

            var expectedStudentValidationException =
                new StudentValidationException(nullStudentException);

            //when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            //then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                addStudentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentIsInvalidAndLogItAsync(string invalidFirstName)
        {
            //given
            Student invalidStudent = new Student
            {
                FirstName = invalidFirstName,
                DateOfBirth = default,
                Status = StudentStatus.Inactive
            };

            var invalidStudentException = new InvalidStudentException();

            invalidStudentException.AddData(
                key: nameof(Student.Id),
                values: "Id is required.");

            invalidStudentException.AddData(
                key: nameof(Student.FirstName),
                values: "Text is required.");

            invalidStudentException.AddData(
                key: nameof(Student.LastName),
                values: "Text is required.");

            invalidStudentException.AddData(
               key: nameof(Student.DateOfBirth),
               values: "Date is required.");

            invalidStudentException.AddData(
               key: nameof(Student.Status),
               values: "Value is invalid.");

            invalidStudentException.AddData(
               key: nameof(Student.CreatedDate),
               values: "Date is required.");

            invalidStudentException.AddData(
               key: nameof(Student.CreatedBy),
               values: "Id is required.");

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            //when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            //then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                addStudentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
