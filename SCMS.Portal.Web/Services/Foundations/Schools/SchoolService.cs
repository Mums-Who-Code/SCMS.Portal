// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Schools;

namespace SCMS.Portal.Web.Services.Foundations.Schools
{
    public class SchoolService : ISchoolService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public SchoolService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IQueryable<School>> RetrieveAllSchools() =>
            throw new System.NotImplementedException();
    }
}
