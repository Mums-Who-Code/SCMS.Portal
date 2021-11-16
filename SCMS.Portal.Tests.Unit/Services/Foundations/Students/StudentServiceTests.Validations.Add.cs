// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            //given
            Student randomStudent = CreateRandomStudent();
            Student invalidStudent = randomStudent;
            invalidStudent.UpdateDate = invalidStudent.CreatedDate.AddMinutes(GetRandomNumber());
            var invalidStudentException = new InvalidStudentException();

            invalidStudentException.AddData(
                key: nameof(Student.UpdateDate),
                values: $"Date is not same as {nameof(Student.CreatedDate)}.");

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            //when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            //then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                addStudentTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never());

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedByIsNotSameAsUpdatedByAndLogItAsync()
        {
            //given
            Student randomStudent = CreateRandomStudent();
            Student invalidStudent = randomStudent;
            invalidStudent.UpdatedBy = Guid.NewGuid();
            var invalidStudentException = new InvalidStudentException();

            invalidStudentException.AddData(
                key: nameof(Student.UpdatedBy),
                values: $"Id is not same as {nameof(Student.CreatedBy)}.");

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            //when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            //then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                addStudentTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never());

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InvalidMinuteCases))]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotRecentAndLogItAsync(
            int invalidMinutes)
        {
            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent(randomDateTime);
            Student invalidStudent = randomStudent;
            invalidStudent.CreatedDate = invalidStudent.CreatedDate.AddMinutes(invalidMinutes);
            invalidStudent.UpdateDate = invalidStudent.CreatedDate;
            var invalidStudentException = new InvalidStudentException();

            invalidStudentException.AddData(
                key: nameof(Student.CreatedDate),
                values: "Date is not recent");

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            //when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            //then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                addStudentTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never());

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
