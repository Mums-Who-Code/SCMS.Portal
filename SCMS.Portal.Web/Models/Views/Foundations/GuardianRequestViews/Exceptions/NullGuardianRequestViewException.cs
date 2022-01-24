// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class NullGuardianRequestViewException : Xeption
    {
        public NullGuardianRequestViewException()
            : base("Null guardian request view error occured.")
        { }
    }
}
