// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class NullSchoolViewException : Xeption
    {
        public NullSchoolViewException()
           : base("Null school view error occured.")
        { }
    }
}
