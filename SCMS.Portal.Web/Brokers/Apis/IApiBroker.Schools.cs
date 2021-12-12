// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Schools;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<List<School>> GetAllSchoolsAsync();
    }
}
