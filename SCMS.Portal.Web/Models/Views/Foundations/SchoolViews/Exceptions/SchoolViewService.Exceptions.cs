// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class SchoolViewServiceException : Xeption
    {
        public SchoolViewServiceException(Exception innerException)
            : base(message: "School service error occurred, contact support.",
                  innerException)
        { }
    }
}
