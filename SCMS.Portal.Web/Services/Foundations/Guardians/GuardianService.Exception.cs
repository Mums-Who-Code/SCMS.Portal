// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
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
        }

        private GuardianValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guardianValidationException = new GuardianValidationException(exception);
            this.loggingBroker.LogError(guardianValidationException);

            return guardianValidationException;
        }
    }
}
