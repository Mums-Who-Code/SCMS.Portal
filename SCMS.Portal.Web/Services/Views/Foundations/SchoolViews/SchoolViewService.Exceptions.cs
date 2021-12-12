// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Schools.Exceptions;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using Xeptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewService : ISchoolViewService
    {
        private delegate ValueTask<List<SchoolView>> ReturningSchoolViewsFunction();

        private async ValueTask<List<SchoolView>> TryCatch(ReturningSchoolViewsFunction returningSchoolViewsFunction)
        {
            try
            {
                return await returningSchoolViewsFunction();
            }
            catch (SchoolDependencyException schoolDependencyException)
            {
                throw CreateAndLogDependencyException(
                    schoolDependencyException);
            }
            catch (SchoolServiceException schoolServiceException)
            {
                throw CreateAndLogDependencyException(
                    schoolServiceException);
            }
            catch (Exception exception)
            {
                var failedSchoolViewServiceException =
                    new FailedSchoolViewServiceException(exception);

                throw CreateAndLogServiceException(
                    failedSchoolViewServiceException);
            }
        }

        private SchoolViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var schoolViewDependencyException = new SchoolViewDependencyException(exception);
            this.loggingBroker.LogError(schoolViewDependencyException);

            return schoolViewDependencyException;
        }

        private SchoolViewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var schoolViewServiceException = new SchoolViewServiceException(exception);
            this.loggingBroker.LogError(schoolViewServiceException);

            return schoolViewServiceException;
        }
    }
}
