// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions
{
    public class StudentViewValidationException : Xeption
    {
        public StudentViewValidationException(Xeption innerXeption)
            : base("Student view validation error occured, try again.", innerXeption)
        { }
    }
}
