// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Processings.SchoolViews.Exceptions
{
    public class SchoolViewProcessingServiceException : Xeption
    {
        public SchoolViewProcessingServiceException(Xeption innerException)
            : base(message: "School view error occurred, contact support.",
                  innerException)
        { }
    }
}
