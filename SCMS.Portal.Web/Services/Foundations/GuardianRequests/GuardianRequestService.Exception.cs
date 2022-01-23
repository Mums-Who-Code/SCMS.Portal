// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestService
    {
        private delegate ValueTask<GuardianRequest> ReturningGuardianRequestFunction();

        private async ValueTask<GuardianRequest> TryCatch(ReturningGuardianRequestFunction returningGuardianRequestFunction)
        {
            try
            {
                return await returningGuardianRequestFunction();
            }
            catch (NullGuardianRequestException nullGuardianRequestException)
            {
                throw CreateAndLogValidationException(nullGuardianRequestException);
            }
            catch (InvalidGuardianRequestException invalidGuardianRequestException)
            {
                throw CreateAndLogValidationException(invalidGuardianRequestException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedGuardianRequestDependencyException =
                    new FailedGuardianRequestDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedGuardianRequestDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedGuardianRequestDependencyException =
                    new FailedGuardianRequestDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedGuardianRequestDependencyException);
            }
            catch (HttpResponseUnauthorizedException unauthorizedHttpResponseException)
            {
                var failedGuardianRequestDependencyException =
                    new FailedGuardianRequestDependencyException(unauthorizedHttpResponseException);

                throw CreateAndLogCriticalDependencyException(failedGuardianRequestDependencyException);
            }
            catch (HttpResponseBadRequestException httpBadRquestException)
            {
                var invalidGuardianRequestException =
                    new InvalidGuardianRequestException(
                        httpBadRquestException,
                        httpBadRquestException.Data);

                throw CreateAndLogDependencyValidationException(invalidGuardianRequestException);
            }
            catch (HttpResponseFailedDependencyException httpResponseFailedDependencyException)
            {
                var invalidGuardianRequestException =
                    new InvalidGuardianRequestException(
                        httpResponseFailedDependencyException);

                throw CreateAndLogDependencyValidationException(invalidGuardianRequestException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidGuardianRequestException =
                    new InvalidGuardianRequestException(
                        httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(invalidGuardianRequestException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedGuardianRequestDependencyException =
                    new FailedGuardianRequestDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedGuardianRequestDependencyException);
            }
            catch (Exception serviceException)
            {
                var failedGuardianRequestServiceException =
                    new FailedGuardianRequestServiceException(serviceException);

                throw CreateAndLogServiceException(failedGuardianRequestServiceException);
            }
        }

        private GuardianRequestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var GuardianRequestValidationException = new GuardianRequestValidationException(exception);
            this.loggingBroker.LogError(GuardianRequestValidationException);

            return GuardianRequestValidationException;
        }

        private GuardianRequestDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var GuardianRequestDependencyException = new GuardianRequestDependencyException(exception);
            this.loggingBroker.LogCritical(GuardianRequestDependencyException);

            return GuardianRequestDependencyException;
        }

        private GuardianRequestDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var GuardianRequestDependencyException = new GuardianRequestDependencyException(exception);
            this.loggingBroker.LogError(GuardianRequestDependencyException);

            return GuardianRequestDependencyException;
        }

        private GuardianRequestDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var GuardianRequestDependencyValidationException = new GuardianRequestDependencyValidationException(exception);
            this.loggingBroker.LogError(GuardianRequestDependencyValidationException);

            return GuardianRequestDependencyValidationException;
        }

        private GuardianRequestServiceException CreateAndLogServiceException(Xeption exception)
        {
            var GuardianRequestServiceException = new GuardianRequestServiceException(exception);
            this.loggingBroker.LogError(GuardianRequestServiceException);

            return GuardianRequestServiceException;
        }
    }
}
