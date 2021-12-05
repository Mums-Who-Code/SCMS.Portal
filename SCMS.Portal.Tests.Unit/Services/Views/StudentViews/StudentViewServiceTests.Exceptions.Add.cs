// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfValidationErrorOccuredAndLogItAsync(
            Exception studentServiceValidationException)
        {
            //given
            StudentView someStudentView = CreateRandomStudentView();

            var expectedDependencyValidationException =
                new StudentViewDependencyValidationException(studentServiceValidationException.InnerException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(studentServiceValidationException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewDependencyValidationException>(() =>
                addStudentViewTask.AsTask());

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccuredAndLogItAsync(
            Exception studentServiceDependencyException)
        {
            //given
            StudentView someStudentView = CreateRandomStudentView();

            var expectedDependencyException =
                new StudentViewDependencyException(studentServiceDependencyException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(studentServiceDependencyException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewDependencyException>(() =>
                addStudentViewTask.AsTask());

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccuredAndLogItAsync()
        {
            //given
            StudentView someStudentView = CreateRandomStudentView();

            var serviceException = new Exception();

            var failedStudentViewServiceException =
                new FailedStudentViewServiceException(serviceException);

            var studentViewServiceException =
                new StudentViewServiceException(failedStudentViewServiceException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(serviceException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewServiceException>(() =>
                addStudentViewTask.AsTask());

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    studentViewServiceException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }

    }
}
