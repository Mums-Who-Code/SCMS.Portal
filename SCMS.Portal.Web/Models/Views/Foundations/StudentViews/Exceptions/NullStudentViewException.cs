// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions
{
    public class NullStudentViewException : Xeption
    {
        public NullStudentViewException()
            : base("Null student view error occured.")
        { }
    }
}
