// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class FailedSchoolViewDependencyException : Xeption
    {
        public FailedSchoolViewDependencyException(Xeption innerException)
            : base(message: "Failed school dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
