// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Student someStudent = CreateRandomStudent();
            SqlException sqlException = GetSqlException();

            var failedStudentStorageException =
                new FailedStudentStorageException(sqlException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(someStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
               addStudentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
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
