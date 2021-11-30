// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SCMS.Portal.Web.Views.Bases.Cards
{
    public partial class CardBase : ComponentBase
    {
        [Parameter]
        public RenderFragment Content { get; set; }
    }
}
