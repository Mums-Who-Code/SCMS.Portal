// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewService
    {
        private delegate ValueTask<GuardianRequestView> ReturningGuardianRequestViewFunction();

        private async ValueTask<GuardianRequestView> TryCatch(ReturningGuardianRequestViewFunction returningGuardianRequestViewFunction)
        {
            try
            {
                return await returningGuardianRequestViewFunction();
            }
            catch (NullGuardianRequestViewException nullGuardianRequestViewException)
            {
                throw CreateAndLogValidationException(nullGuardianRequestViewException);
            }
            catch (InvalidGuardianRequestViewException invalidGuardianRequestViewException)
            {
                throw CreateAndLogValidationException(invalidGuardianRequestViewException);
            }
            catch (GuardianRequestValidationException guardianRequestValidationException)
            {
                throw CreateAndLogDependencyValidationException(guardianRequestValidationException);
            }
            catch (GuardianRequestDependencyValidationException guardianRequestDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(guardianRequestDependencyValidationException);
            }
        }

        private GuardianRequestViewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guardianRequestViewValidationException = new GuardianRequestViewValidationException(exception);
            this.loggingBroker.LogError(guardianRequestViewValidationException);

            return guardianRequestViewValidationException;
        }

        private GuardianRequestViewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var guardianRequestViewDependencyValidationException = new GuardianRequestViewDependencyValidationException(exception.InnerException);
            this.loggingBroker.LogError(guardianRequestViewDependencyValidationException);

            return guardianRequestViewDependencyValidationException;
        }
    }
}
