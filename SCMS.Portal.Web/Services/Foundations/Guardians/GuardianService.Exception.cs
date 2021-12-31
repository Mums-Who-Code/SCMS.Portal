// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

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
    }
}
