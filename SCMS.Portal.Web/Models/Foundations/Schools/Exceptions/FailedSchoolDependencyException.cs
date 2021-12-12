// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Schools.Exceptions
{
    public class FailedSchoolDependencyException : Xeption
    {
        public FailedSchoolDependencyException(Exception innerException)
            : base(message: "Failed school dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
