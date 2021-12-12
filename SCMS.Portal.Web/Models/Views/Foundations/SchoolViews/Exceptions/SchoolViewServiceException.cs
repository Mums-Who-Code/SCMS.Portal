// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class SchoolViewServiceException : Xeption
    {
        public SchoolViewServiceException(Xeption innerException)
            : base(message: "School service error occurred, contact support.",
                  innerException)
        { }
    }
}
