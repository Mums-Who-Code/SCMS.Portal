// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class GuardianRequestViewServiceException : Xeption
    {
        public GuardianRequestViewServiceException(Xeption innerException)
    : base(message: "Guardian Request view service error occurred, contact support.", innerException) { }

    }
}
