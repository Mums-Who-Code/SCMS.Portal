// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class GuardianRequestValidationException : Xeption
    {
        public GuardianRequestValidationException(Xeption innerException)
            : base("Guardian validation error occured, try again.", innerException) { }
    }
}
