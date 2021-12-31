// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class GuardianDependencyException : Xeption
    {
        public GuardianDependencyException(Xeption innerException)
            : base(message: "Guardian dependency error occured, contact support.", innerException)
        { }
    }
}
