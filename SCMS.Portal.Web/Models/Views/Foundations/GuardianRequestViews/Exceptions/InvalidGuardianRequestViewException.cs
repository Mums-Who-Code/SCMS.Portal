// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class InvalidGuardianRequestViewException : Xeption
    {
        public InvalidGuardianRequestViewException()
            : base("Invalid guardianView error occured, fix errors and try again.")
        { }
    }
}
