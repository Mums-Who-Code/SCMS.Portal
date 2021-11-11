// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Students.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Foundations.Students
{
    public partial class StudentService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction();
            }
            catch (NullStudentException nullStudentException)
            {
                throw CreateAndLogValidationException(nullStudentException);
            }
            catch (InvalidStudentException invalidStudentException)
            {
                throw CreateAndLogValidationException(invalidStudentException);
            }
        }

        private StudentValidationException CreateAndLogValidationException(Xeption nullStudentException)
        {
            var studentValidationException = new StudentValidationException(nullStudentException);
            this.loggingBroker.LogError(studentValidationException);

            return studentValidationException;
        }
    }
}
