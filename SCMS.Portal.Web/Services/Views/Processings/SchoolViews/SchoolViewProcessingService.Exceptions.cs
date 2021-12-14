// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using SCMS.Portal.Web.Models.Views.Processings.SchoolViews.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Views.Processings.SchoolViews
{
    public partial class SchoolViewProcessingService : ISchoolViewProcessingService
    {
        private delegate ValueTask<List<SchoolView>> ReturningSchoolViewsFunction();

        private async ValueTask<List<SchoolView>> TryCatch(ReturningSchoolViewsFunction returningSchoolViewsFunction)
        {
            try
            {
                return await returningSchoolViewsFunction();
            }
            catch (SchoolViewDependencyException schoolViewDependencyException)
            {
                throw CreateAndLogDependencyException(schoolViewDependencyException);
            }
            catch (SchoolViewServiceException schoolViewServiceException)
            {
                throw CreateAndLogDependencyException(schoolViewServiceException);
            }
            catch (Exception exception)
            {
                var failedSchoolViewProcessingException =
                    new FailedSchoolViewProcessingException(exception);

                throw CreateAndLogServiceException(failedSchoolViewProcessingException);
            }
        }

        private SchoolViewProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var schoolViewProcessingDependencyException =
                new SchoolViewProcessingDependencyException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(schoolViewProcessingDependencyException);

            throw schoolViewProcessingDependencyException;
        }

        private SchoolViewProcessingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var schoolViewProcessingServiceException =
                new SchoolViewProcessingServiceException(exception);

            this.loggingBroker.LogError(schoolViewProcessingServiceException);

            throw schoolViewProcessingServiceException;
        }
    }
}
