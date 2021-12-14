// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;

namespace SCMS.Portal.Web.Services.Views.Processings.SchoolViews
{
    public class SchoolViewProcessingService : ISchoolViewProcessingService
    {
        private readonly ISchoolViewService schoolViewService;
        private readonly ILoggingBroker loggingBroker;

        public SchoolViewProcessingService(
            ISchoolViewService schoolViewService,
            ILoggingBroker loggingBroker)
        {
            this.schoolViewService = schoolViewService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<List<SchoolView>> RetrieveAllSchoolViewsAsync() =>
            await this.schoolViewService.RetrieveAllSchoolViewsAsync();
    }
}
