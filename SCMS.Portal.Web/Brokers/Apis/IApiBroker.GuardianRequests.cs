// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<GuardianRequest> PostGuardianRequestAsync(GuardianRequest guardianRequest);
    }
}
