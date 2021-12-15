// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;

namespace SCMS.Portal.Web.Views.Bases.Dropdowns.AutoCompletes
{
    public partial class DropdownAutoCompleteBase<TClass> : ComponentBase
    {
        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        private bool IsEnabled => IsDisabled is false;

        [Parameter]
        public List<TClass> Items { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        public async Task SetValue(string value)
        {
            this.Value = value;
            await ValueChanged.InvokeAsync(value);
        }

        public async Task OnValueChanged(
            ChangeEventArgs<string, TClass> changeEventArgs)
        {
            await SetValue(changeEventArgs.Value);
        }

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
