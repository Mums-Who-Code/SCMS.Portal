// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Processings.SchoolViews.Exceptions
{
    public class FailedSchoolViewProcessingException : Xeption
    {
        public FailedSchoolViewProcessingException(Exception innerException)
            : base(message: "Failed school view error occurred, contact support.",
                  innerException)
        { }
    }
}
