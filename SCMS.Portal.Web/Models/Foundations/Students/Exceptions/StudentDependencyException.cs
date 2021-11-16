// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Students.Exceptions
{
    public class StudentDependencyException : Xeption
    {
        public StudentDependencyException(Xeption innerException)
            : base(message: "Student dependency error occurred, contact support.", innerException)
        { }
    }
}
