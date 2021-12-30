// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions
{
    public class InvalidGuardianException : Xeption
    {
        public InvalidGuardianException()
            : base(message: "Invalid guardian, fix the errors and try again.")
        { }

        public InvalidGuardianException(Exception innerException, IDictionary data)
            : base(message: "Invalid guardian, fix the errors and try again.",
                  innerException,
                  data)
        { }

        public InvalidGuardianException(Exception innerException)
            : base(message: "Invalid guardian, fix the errors and try again.",
                  innerException)
        { }
    }
}
