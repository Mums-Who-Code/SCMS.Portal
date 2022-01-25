// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class GuardianRequestViewDependencyException : Xeption
    {
        public GuardianRequestViewDependencyException(Exception innerException)
     : base("Guardian Request view dependency error occured, contact support.", innerException)
        { }
    }
}
