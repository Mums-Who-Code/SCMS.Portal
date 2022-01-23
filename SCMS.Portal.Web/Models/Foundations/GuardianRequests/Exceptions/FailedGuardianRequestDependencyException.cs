// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class FailedGuardianRequestDependencyException : Xeption
    {
        public FailedGuardianRequestDependencyException(Exception innerException)
            : base(message: "Failed guardian request dependency error occured.", innerException)
        { }
    }
}
