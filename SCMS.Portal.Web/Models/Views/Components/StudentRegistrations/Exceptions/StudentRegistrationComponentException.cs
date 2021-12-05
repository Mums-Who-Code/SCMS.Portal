// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Components.StudentRegistrations.Exceptions
{
    public class StudentRegistrationComponentException : Xeption
    {
        public StudentRegistrationComponentException(Xeption innerException)
            : base(message: "Student registration error occurred, contact support.", innerException)
        { }
    }
}
