// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Students.Exceptions
{
    public class StudentServiceException : Xeption
    {
        public StudentServiceException(Xeption innerException)
            : base(message: "Student service error occurred, contact support.", innerException) { }
    }
}
