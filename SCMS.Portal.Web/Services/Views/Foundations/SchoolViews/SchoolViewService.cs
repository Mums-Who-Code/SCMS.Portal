// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Brokers.Navigations;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Foundations.Schools;

namespace SCMS.Portal.Web.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewService : ISchoolViewService
    {
        private readonly ISchoolService schoolService;
        private readonly ILoggingBroker loggingBroker;
        private readonly INavigationBroker navigationBroker;

        public SchoolViewService(
            ISchoolService schoolService,
            INavigationBroker navigationBroker,
            ILoggingBroker loggingBroker)
        {
            this.schoolService = schoolService;
            this.navigationBroker = navigationBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<SchoolView>> RetrieveAllSchoolViewsAsync() =>
        TryCatch(async () =>
        {
            List<School> schools =
                await this.schoolService.RetrieveAllSchoolsAsync();

            return schools.Select(AsSchoolView).ToList();
        });

        public void NavigateTo(string route) =>
            throw new NotImplementedException();

        private static Func<School, SchoolView> AsSchoolView =>
            school => new SchoolView
            {
                Id = school.Id,
                Name = school.Name
            };
    }
}
