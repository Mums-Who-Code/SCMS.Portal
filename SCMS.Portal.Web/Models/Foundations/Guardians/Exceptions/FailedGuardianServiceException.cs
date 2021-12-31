// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class FailedGuardianServiceException : Xeption
    {
        public FailedGuardianServiceException(Exception innerException)
            : base(message: "Failed student service error occured.", innerException)
        { }
    }
}
