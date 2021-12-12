// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Models.Foundations.Schools.Exceptions;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using SCMS.Portal.Web.Services.Foundations.Schools;
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
            catch(SchoolServiceException schoolServiceException)
            {
                throw CreateAndLogDependencyException(
                    schoolServiceException);
            }
        }

        private SchoolViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var schoolViewDependencyException = new SchoolViewDependencyException(exception);
            this.loggingBroker.LogError(schoolViewDependencyException);

            return schoolViewDependencyException;
        }
    }
}
