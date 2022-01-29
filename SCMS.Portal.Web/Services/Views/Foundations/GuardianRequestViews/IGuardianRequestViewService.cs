// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;

namespace SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews
{
    public interface IGuardianRequestViewService
    {
        ValueTask<GuardianRequestView> AddGuardianRequestViewAsync(GuardianRequestView guardianRequestView);
    }
}
