// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Models.Foundations.Guardians;
using SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Foundations.Guardians
{
    public partial class GuardianService
    {
        private delegate ValueTask<Guardian> ReturningGuardianFunction();

        private async ValueTask<Guardian> TryCatch(ReturningGuardianFunction returningGuardianFunction)
        {
            try
            {
                return await returningGuardianFunction();
            }
            catch (NullGuardianException nullGuardianException)
            {
                throw CreateAndLogValidationException(nullGuardianException);
            }
            catch (InvalidGuardianException invalidGuardianException)
            {
                throw CreateAndLogValidationException(invalidGuardianException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedGuardianDependencyException =
                    new FailedGuardianDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedGuardianDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedGuardianDependencyException =
                    new FailedGuardianDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedGuardianDependencyException);
            }
            catch (HttpResponseUnauthorizedException unauthorizedHttpResponseException)
            {
                var failedGuardianDependencyException =
                    new FailedGuardianDependencyException(unauthorizedHttpResponseException);

                throw CreateAndLogCriticalDependencyException(failedGuardianDependencyException);
            }
            catch (HttpResponseBadRequestException httpBadRquestException)
            {
                var invalidGuardianException =
                    new InvalidGuardianException(
                        httpBadRquestException,
                        httpBadRquestException.Data);

                throw CreateAndLogDependencyValidationException(invalidGuardianException);
            }
            catch (HttpResponseFailedDependencyException httpResponseFailedDependencyException)
            {
                var invalidGuardianException =
                    new InvalidGuardianException(
                        httpResponseFailedDependencyException);

                throw CreateAndLogDependencyValidationException(invalidGuardianException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidGuardianException =
                    new InvalidGuardianException(
                        httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(invalidGuardianException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedGuardianDependencyException =
                    new FailedGuardianDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedGuardianDependencyException);
            }
            catch (Exception serviceException)
            {
                var failedGuardianServiceException =
                    new FailedGuardianServiceException(serviceException);

                throw CreateAndLogServiceException(failedGuardianServiceException);
            }
        }

        private GuardianValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guardianValidationException = new GuardianValidationException(exception);
            this.loggingBroker.LogError(guardianValidationException);

            return guardianValidationException;
        }

        private GuardianDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var guardianDependencyException = new GuardianDependencyException(exception);
            this.loggingBroker.LogCritical(guardianDependencyException);

            return guardianDependencyException;
        }

        private GuardianDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var guardianDependencyException = new GuardianDependencyException(exception);
            this.loggingBroker.LogError(guardianDependencyException);

            return guardianDependencyException;
        }

        private GuardianDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var guardianDependencyValidationException = new GuardianDependencyValidationException(exception);
            this.loggingBroker.LogError(guardianDependencyValidationException);

            return guardianDependencyValidationException;
        }

        private GuardianServiceException CreateAndLogServiceException(Xeption exception)
        {
            var guardianServiceException = new GuardianServiceException(exception);
            this.loggingBroker.LogError(guardianServiceException);

            return guardianServiceException;
        }
    }
}
