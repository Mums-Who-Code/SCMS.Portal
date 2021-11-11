// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Services.Foundations.Students.Exceptions
{
    public class InvalidStudentException : Xeption
    {
        public InvalidStudentException(string parameterName, object parameterValue)
            : base("Invalid student error occured, " +
                  $"parameter name:{parameterName}" +
                  $"parameter value:{parameterValue}")
        { }
    }
}
