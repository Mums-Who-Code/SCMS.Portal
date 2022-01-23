// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;

namespace SCMS.Portal.Web.Services.Foundations.GuardianRequests
{
    public interface IGuardianRequestService
    {
        ValueTask<GuardianRequest> AddGuardianRequestAsync(GuardianRequest guardianRequest);
    }
}
