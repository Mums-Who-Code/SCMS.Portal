// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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
        private delegate void ReturningNothingFunction();

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
            catch (GuardianRequestDependencyException guardianRequestDependencyException)
            {
                throw CreateAndLogDependencyException(guardianRequestDependencyException);
            }
            catch (GuardianRequestServiceException guardianRequestServiceException)
            {
                throw CreateAndLogDependencyException(guardianRequestServiceException);
            }
            catch (Exception serviceException)
            {
                var failedGuardianRequestViewServiceException
                    = new FailedGuardianRequestViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedGuardianRequestViewServiceException);
            }
        }

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (InvalidGuardianRequestViewException invalidStudentViewException)
            {
                throw CreateAndLogValidationException(invalidStudentViewException);
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

        private GuardianRequestViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var guardianRequestViewDependencyException = new GuardianRequestViewDependencyException(exception);
            this.loggingBroker.LogError(guardianRequestViewDependencyException);

            return guardianRequestViewDependencyException;
        }

        private GuardianRequestViewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var guardianRequestViewServiceException = new GuardianRequestViewServiceException(exception);
            this.loggingBroker.LogError(guardianRequestViewServiceException);

            return guardianRequestViewServiceException;
        }
    }
}
