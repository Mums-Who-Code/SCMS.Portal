// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public class InvalidSchoolViewException : Xeption
    {
        public InvalidSchoolViewException()
            : base("Invalid school view error occured, fix errors and try again.")
        { }

        public InvalidSchoolViewException(string parameterName, object parameterValue)
           : base($"Invalid school view error occured. " +
                $"parameter name: {parameterName}, " +
                $"parameter value: {parameterValue}")
        { }
    }
}
