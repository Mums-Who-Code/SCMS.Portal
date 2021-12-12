// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class FailedSchoolViewServiceException : Xeption
    {
        public FailedSchoolViewServiceException(Exception innerException)
            : base(message: "Failed school service error occurred, contact support.",
                  innerException)
        { }
    }
}
