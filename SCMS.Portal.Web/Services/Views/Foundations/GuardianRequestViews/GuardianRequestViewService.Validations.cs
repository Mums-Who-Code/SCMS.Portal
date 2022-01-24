// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewService
    {
        private void ValidateGuardianRequestViewOnAdd(GuardianRequestView guardianRequestView)
        {
            ValidateInput(guardianRequestView);
        }

        private void ValidateInput(GuardianRequestView guardianRequestView)
        {
            if (guardianRequestView == null)
            {
                throw new NullGuardianRequestViewException();
            }
        }
    }
}
