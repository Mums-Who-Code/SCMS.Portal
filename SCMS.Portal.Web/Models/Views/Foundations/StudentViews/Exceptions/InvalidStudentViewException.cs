// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions
{
    public class InvalidStudentViewException : Xeption
    {
        public InvalidStudentViewException()
            : base("Invalid student view error occured, fix errors and try again.")
        { }

        public InvalidStudentViewException(string parameterName, object parameterValue)
            : base($"Invalid student view error occured. " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}")
        { }
    }
}
