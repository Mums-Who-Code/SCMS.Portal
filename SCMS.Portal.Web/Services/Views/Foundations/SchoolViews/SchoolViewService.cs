// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Foundations.Schools;

namespace SCMS.Portal.Web.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewService : ISchoolViewService
    {
        private readonly ISchoolService schoolService;
        private readonly ILoggingBroker loggingBroker;

        public SchoolViewService(
            ISchoolService schoolService,
            ILoggingBroker loggingBroker)
        {
            this.schoolService = schoolService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<SchoolView>> RetrieveAllSchoolsAsync() =>
        TryCatch(async() =>
        {
            List<School> schools =
                await this.schoolService.RetrieveAllSchoolsAsync();

            return schools.Select(AsSchoolView).ToList();
        });

        private static Func<School, SchoolView> AsSchoolView =>
            school => new SchoolView
            {
                Id = school.Id,
                Name = school.Name
            };
    }
}
