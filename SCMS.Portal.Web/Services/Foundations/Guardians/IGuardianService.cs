// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Guardians;

namespace SCMS.Portal.Web.Services.Foundations.Guardians
{
    public interface IGuardianService
    {
        ValueTask<Guardian> AddGuardianAsync(Guardian guardian);
    }
}
