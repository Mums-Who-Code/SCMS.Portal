// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class FailedGuardianRequestViewServiceException : Xeption
    {
        public FailedGuardianRequestViewServiceException(Exception innerException)
    : base(message: "Failed guardian request view service error occurred.", innerException)
        { }

    }
}
