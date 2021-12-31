// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class FailedGuardianDependencyException : Xeption
    {
        public FailedGuardianDependencyException(Exception innerException)
            : base(message: "Failed guardian dependency error occured.", innerException)
        { }
    }
}
