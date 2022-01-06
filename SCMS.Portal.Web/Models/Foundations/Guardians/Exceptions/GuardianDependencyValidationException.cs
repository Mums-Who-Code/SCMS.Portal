// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class GuardianDependencyValidationException : Xeption
    {
        public GuardianDependencyValidationException(Xeption innerException)
            : base(message: "Guardian dependency validation error occured, contact support.", innerException)
        { }
    }
}
