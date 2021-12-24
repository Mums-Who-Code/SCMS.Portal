// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Portal.Web.Models.Foundations.Guardians;
using SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions;

namespace SCMS.Portal.Web.Services.Foundations.Guardians
{
    public partial class GuardianService
    {
        private void ValidateGuardianOnAdd(Guardian guardian)
        {
            ValidateInput(guardian);
        }

        private void ValidateInput(Guardian guardian)
        {
            if (guardian == null)
            {
                throw new NullGuardianException();
            }
        }
    }
}
