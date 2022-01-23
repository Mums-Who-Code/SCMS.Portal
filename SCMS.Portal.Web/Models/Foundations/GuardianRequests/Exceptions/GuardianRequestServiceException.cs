// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class GuardianRequestServiceException : Xeption
    {
        public GuardianRequestServiceException(Xeption innerException)
            : base(message: "Guardian request service error occured, contact support.", innerException)
        { }
    }
}
