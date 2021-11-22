// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.StudentViews.Exceptions
{
    public class InvalidStudentViewException : Xeption
    {
        public InvalidStudentViewException()
            : base("Invalid student view error occured, fix errors and try again.")
        { }
    }
}
