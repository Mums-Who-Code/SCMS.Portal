// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class SchoolViewDependencyException : Xeption
    {
        public SchoolViewDependencyException(Xeption innerException)
            : base(message: "School dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
