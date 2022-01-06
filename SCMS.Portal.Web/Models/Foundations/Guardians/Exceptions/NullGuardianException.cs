// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class NullGuardianException : Xeption
    {
        public NullGuardianException()
            : base(message: "Null guardian error occured.") { }
    }
}
