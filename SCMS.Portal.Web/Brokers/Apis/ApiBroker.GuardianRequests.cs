// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private const string GuardianRequestsRelativeUrl = "api/guardianRequests";

        public async ValueTask<GuardianRequest> PostGuardianRequestAsync(GuardianRequest guardianRequest) =>
            await this.PostAsync(GuardianRequestsRelativeUrl, guardianRequest);
    }
}
