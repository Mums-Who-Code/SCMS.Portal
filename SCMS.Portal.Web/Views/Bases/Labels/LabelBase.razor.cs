// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SCMS.Portal.Web.Views.Bases.Labels
{
    public partial class LabelBase : ComponentBase
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Color { get; set; }

        public void SetValue(string value) =>
            this.Value = value;

        public void SetColor(string color) =>
            this.Color = color;
    }
}
