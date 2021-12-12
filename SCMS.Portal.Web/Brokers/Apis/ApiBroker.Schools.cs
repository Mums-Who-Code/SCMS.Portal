// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Schools;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private const string SchoolsRelativeUrl = "api/schools";

        public async ValueTask<IQueryable<School>> GetAllSchoolsAsync() =>
            await this.GetAsync<IQueryable<School>>(SchoolsRelativeUrl);
    }
}
