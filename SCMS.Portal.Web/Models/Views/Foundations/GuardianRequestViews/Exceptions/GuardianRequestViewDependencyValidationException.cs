// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class GuardianRequestViewDependencyValidationException : Xeption
    {
        public GuardianRequestViewDependencyValidationException(Exception innerException)
            : base("Guardian request view dependency validation error occured, try again.", innerException)
        { }

    }
}
