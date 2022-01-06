// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class GuardianValidationException : Xeption
    {
        public GuardianValidationException(Xeption innerException)
            : base("Guardian validation error occured, try again.", innerException) { }
    }
}
