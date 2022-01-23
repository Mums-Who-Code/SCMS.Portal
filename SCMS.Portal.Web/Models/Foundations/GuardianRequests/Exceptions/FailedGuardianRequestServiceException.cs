// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class FailedGuardianRequestServiceException : Xeption
    {
        public FailedGuardianRequestServiceException(Exception innerException)
            : base(message: "Failed guardian request service error occured.", innerException)
        { }
    }
}
