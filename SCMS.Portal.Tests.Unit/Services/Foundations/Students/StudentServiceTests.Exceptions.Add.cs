// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            Student someStudent = CreateRandomStudent();

            var failedStudentDependencyException =
                new FailedStudentDependencyException(criticalDependencyException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
               addStudentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ApiDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfErrorOccursAndLogItAsync(
                Exception apiDependencyException)
        {
            // given
            Student someStudent = CreateRandomStudent();

            var failedStudentDependencyException =
                new FailedStudentDependencyException(apiDependencyException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(apiDependencyException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
               addStudentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptiononOnAddIfBadRequestAndLogItAsync()
        {
            // given
            IDictionary randomDictionary = CreateRandomDictionary();
            IDictionary exceptionData = randomDictionary;
            string randomMessage = GetRandomMessage();
            string responseMessage = randomMessage;
            var httpResponseMessage = new HttpResponseMessage();
            Student someStudent = CreateRandomStudent();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    httpResponseMessage,
                    responseMessage);

            httpResponseBadRequestException.AddData(exceptionData);

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            var invalidStudentException =
                new InvalidStudentException(
                    httpResponseBadRequestException,
                    exceptionData);

            var expectedStudentDependencyException =
                new StudentDependencyValidationException(invalidStudentException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyValidationException>(() =>
               addStudentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptiononOnAddIfValidationErrorOccursAndLogItAsync(
            Exception dependencyValidationException)
        {
            // given
            IDictionary randomDictionary = CreateRandomDictionary();
            IDictionary exceptionData = randomDictionary;
            string randomMessage = GetRandomMessage();
            string responseMessage = randomMessage;
            var httpResponseMessage = new HttpResponseMessage();
            Student someStudent = CreateRandomStudent();

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(dependencyValidationException);

            var invalidStudentException =
                new InvalidStudentException(
                    dependencyValidationException);

            var expectedStudentDependencyException =
                new StudentDependencyValidationException(invalidStudentException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyValidationException>(() =>
               addStudentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptiononOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();
            Student someStudent = CreateRandomStudent();

            var failedStudentServiceException =
                new FailedStudentServiceException(serviceException);

            var expectedStudentServiceException =
                new StudentServiceException(failedStudentServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentServiceException>(() =>
               addStudentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentServiceException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
