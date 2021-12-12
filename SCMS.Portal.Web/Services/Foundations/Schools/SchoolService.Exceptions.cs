// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Models.Foundations.Schools.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Foundations.Schools
{
    public partial class SchoolService
    {
        private delegate ValueTask<IQueryable<School>> RetunringSchoolsFunction();

        private async ValueTask<IQueryable<School>> TryCatch(RetunringSchoolsFunction retunringSchoolsFunction)
        {
            try
            {
                return await retunringSchoolsFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedSchoolDependencyException =
                    new FailedSchoolDependencyException(
                        httpRequestException);

                throw CreateAndLogCriticalDependencyException(
                    failedSchoolDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedSchoolDependencyException =
                    new FailedSchoolDependencyException(
                        httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(
                    failedSchoolDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedSchoolDependencyException =
                    new FailedSchoolDependencyException(
                        httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(
                    failedSchoolDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedSchoolDependencyException =
                    new FailedSchoolDependencyException(
                        httpResponseException);

                throw CreateAndLogDependencyException(
                    failedSchoolDependencyException);
            }
            catch (Exception exception)
            {
                var failedSchoolServiceException =
                    new FailedSchoolServiceException(exception);

                throw CreateAndLogServiceException(
                    failedSchoolServiceException);
            }
        }

        private SchoolDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var schoolDependencyException = new SchoolDependencyException(exception);
            this.loggingBroker.LogCritical(schoolDependencyException);

            throw schoolDependencyException;
        }

        private SchoolDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var schoolDependencyException = new SchoolDependencyException(exception);
            this.loggingBroker.LogError(schoolDependencyException);

            throw schoolDependencyException;
        }

        private SchoolServiceException CreateAndLogServiceException(Xeption exception)
        {
            var schoolServiceException = new SchoolServiceException(exception);
            this.loggingBroker.LogError(schoolServiceException);

            throw schoolServiceException;
        }
    }
}
