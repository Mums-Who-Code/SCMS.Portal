// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Views.StudentViews
{
    public partial class StudentViewService
    {
        private delegate ValueTask<StudentView> ReturningStudentViewFunction();

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
        }

        private StudentViewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentViewValidationException = new StudentViewValidationException(exception);
            this.loggingBroker.LogError(studentViewValidationException);

            return studentViewValidationException;
        }

        private StudentViewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var studentViewDependencyValidationException = new StudentViewDependencyValidationException(exception);
            this.loggingBroker.LogError(studentViewDependencyValidationException);

            return studentViewDependencyValidationException;
        }

        private StudentViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentViewDependencyException = new StudentViewDependencyException(exception);
            this.loggingBroker.LogError(studentViewDependencyException);

            return studentViewDependencyException;
        }
    }
}
