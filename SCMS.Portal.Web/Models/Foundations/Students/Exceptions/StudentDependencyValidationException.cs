// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Students.Exceptions
{
    public class StudentDependencyValidationException : Xeption
    {
        public StudentDependencyValidationException(Xeption innerException)
            : base(message: "Student dependency validation error occurred, contact support.", innerException)
        { }
    }
}
