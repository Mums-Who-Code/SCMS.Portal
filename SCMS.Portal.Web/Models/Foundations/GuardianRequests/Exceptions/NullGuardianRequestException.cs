// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class NullGuardianRequestException : Xeption
    {
        public NullGuardianRequestException()
            : base(message: "Null guardian request error occured.")
        { }
    }
}
