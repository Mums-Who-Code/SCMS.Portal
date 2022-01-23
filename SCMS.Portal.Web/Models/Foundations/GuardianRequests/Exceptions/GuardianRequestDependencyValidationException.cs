// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class GuardianRequestDependencyValidationException : Xeption
    {
        public GuardianRequestDependencyValidationException(Xeption innerException)
            : base(message: "Guardian request dependency validation error occured, contact support.", innerException)
        { }
    }
}
