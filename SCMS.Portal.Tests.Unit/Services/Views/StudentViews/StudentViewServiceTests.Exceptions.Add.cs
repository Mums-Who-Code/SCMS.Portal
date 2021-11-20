// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Theory]
        [MemberData(nameof(StudentServiceValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfStudentValidationErrorOccuredAndLogItAsync(
            Exception studentServiceValidationException)
        {
            //given
            StudentView someStudentView = CreateRandomStudentView();

            var expectedDependencyValidationException =
                new StudentViewDependencyValidationException(studentServiceValidationException);

            this.studentServiceMock.Setup(service =>
                service.AddStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(studentServiceValidationException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewDependencyValidationException>(() =>
                addStudentViewTask.AsTask());

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(StudentServiceDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfStudentDependencyErrorOccuredAndLogItAsync(
            Exception studentServiceDependencyException)
        {
            //given
            StudentView someStudentView = CreateRandomStudentView();

            var expectedDependencyException =
                new StudentViewDependencyException(studentServiceDependencyException);

            this.studentServiceMock.Setup(service =>
                service.AddStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(studentServiceDependencyException);

            //when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            //then
            await Assert.ThrowsAsync<StudentViewDependencyException>(() =>
                addStudentViewTask.AsTask());

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
