// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.StudentViews.Exceptions
{
    public class StudentViewServiceException : Xeption
    {
        public StudentViewServiceException(Xeption innerException)
            : base(message: "Student view service error occurred, contact support.", innerException) { }
    }
}
