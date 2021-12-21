// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Guardians;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private const string GuardiansRelativeUrl = "api/guardians";

        public async ValueTask<Guardian> PostGuardianAsync(Guardian guardian) =>
            await this.PostAsync(GuardiansRelativeUrl, guardian);
    }
}
