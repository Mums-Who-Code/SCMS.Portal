// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewService
    {
        private delegate ValueTask<StudentView> ReturningStudentViewFunction();
        private delegate void ReturningNothingFunction();

        private async ValueTask<StudentView> TryCatch(ReturningStudentViewFunction returningStudentViewFunction)
        {
            try
            {
                return await returningStudentViewFunction();
            }
            catch (NullStudentViewException nullStudentViewException)
            {
                throw CreateAndLogValidationException(nullStudentViewException);
            }
            catch (InvalidStudentViewException invalidStudentViewException)
            {
                throw CreateAndLogValidationException(invalidStudentViewException);
            }
            catch (StudentValidationException studentValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentValidationException);
            }
            catch (StudentDependencyValidationException studentDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentDependencyValidationException);
            }
            catch (StudentDependencyException studentDependencyException)
            {
                throw CreateAndLogDependencyException(studentDependencyException);
            }
            catch (StudentServiceException studentServiceException)
            {
                throw CreateAndLogDependencyException(studentServiceException);
            }
            catch (Exception serviceException)
            {
                var failedStudentViewServiceException =
                    new FailedStudentViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedStudentViewServiceException);
            }
        }

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (InvalidStudentViewException invalidStudentViewException)
            {
                throw CreateAndLogValidationException(invalidStudentViewException);
            }
            catch (Exception serviceException)
            {
                var failedStudentViewServiceException =
                   new FailedStudentViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedStudentViewServiceException);
            }
        }

        private StudentViewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentViewValidationException = new StudentViewValidationException(exception);
            this.loggingBroker.LogError(studentViewValidationException);

            return studentViewValidationException;
        }

        private StudentViewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var studentViewDependencyValidationException = new StudentViewDependencyValidationException(exception.InnerException);
            this.loggingBroker.LogError(studentViewDependencyValidationException);

            return studentViewDependencyValidationException;
        }

        private StudentViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentViewDependencyException = new StudentViewDependencyException(exception);
            this.loggingBroker.LogError(studentViewDependencyException);

            return studentViewDependencyException;
        }

        private StudentViewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var studentViewServiceException = new StudentViewServiceException(exception);
            this.loggingBroker.LogError(studentViewServiceException);

            return studentViewServiceException;
        }
    }
}
