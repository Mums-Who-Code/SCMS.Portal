// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class GuardianRequestDependencyException : Xeption
    {
        public GuardianRequestDependencyException(Xeption innerException)
            : base(message: "Guardian dependency error occured, contact support.", innerException)
        { }
    }
}
