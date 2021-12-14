// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Processings.SchoolViews.Exceptions
{
    public class SchoolViewProcessingDependencyException : Xeption
    {
        public SchoolViewProcessingDependencyException(Xeption innerException)
            : base(message: "School view dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
