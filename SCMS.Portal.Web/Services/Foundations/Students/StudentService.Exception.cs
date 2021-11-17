// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
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
            catch (HttpRequestException httpRequestException)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedStudentDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                FailedStudentDependencyException failedStudentDependencyException =
                    new FailedStudentDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedStudentDependencyException);
            }
            catch (HttpResponseUnauthorizedException unauthorizedHttpResponseException)
            {
                FailedStudentDependencyException failedStudentDependencyException =
                    new FailedStudentDependencyException(unauthorizedHttpResponseException);

                throw CreateAndLogCriticalDependencyException(failedStudentDependencyException);
            }
            catch (HttpResponseBadRequestException httpBadRequestException)
            {
                var invalidStudentException =
                    new InvalidStudentException(
                        httpBadRequestException,
                        httpBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidStudentException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidStudentException =
                    new InvalidStudentException(
                        httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(invalidStudentException);
            }
            catch (HttpResponseFailedDependencyException httpResponseFailedDependencyException)
            {
                var invalidStudentException =
                    new InvalidStudentException(
                        httpResponseFailedDependencyException);

                throw CreateAndLogDependencyValidationException(invalidStudentException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedStudentDependencyException);
            }
            catch (Exception exception)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(exception);

                throw CreateAndLogDependencyException(failedStudentDependencyException);
            }
        }

        private StudentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentValidationException = new StudentValidationException(exception);
            this.loggingBroker.LogError(studentValidationException);

            return studentValidationException;
        }

        private StudentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);
            this.loggingBroker.LogCritical(studentDependencyException);

            return studentDependencyException;
        }

        private StudentDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);
            this.loggingBroker.LogError(studentDependencyException);

            return studentDependencyException;
        }

        private StudentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var studentDependencyValidationException = new StudentDependencyValidationException(exception);
            this.loggingBroker.LogError(studentDependencyValidationException);

            return studentDependencyValidationException;
        }
    }
}
