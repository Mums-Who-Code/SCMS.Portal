// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions
{
    public class InvalidGuardianRequestException : Xeption
    {
        public InvalidGuardianRequestException()
            : base(message: "Invalid guardian request, fix the errors and try again.")
        { }

        public InvalidGuardianRequestException(Exception innerException, IDictionary data)
            : base(message: "Invalid guardian request, fix the errors and try again.",
                  innerException,
                  data)
        { }

        public InvalidGuardianRequestException(Exception innerException)
            : base(message: "Invalid guardian request, fix the errors and try again.",
                  innerException)
        { }
    }
}
