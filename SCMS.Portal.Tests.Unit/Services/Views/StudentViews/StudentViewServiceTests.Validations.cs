// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;
using SCMS.Portal.Web.Services.Views.StudentViews;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.StudentViews
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

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
