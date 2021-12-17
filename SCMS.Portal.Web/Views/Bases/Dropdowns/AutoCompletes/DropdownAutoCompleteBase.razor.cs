// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;

namespace SCMS.Portal.Web.Views.Bases.Dropdowns.AutoCompletes
{
    public partial class DropdownAutoCompleteBase<T> : ComponentBase
    {
        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        private bool IsEnabled => IsDisabled is false;

        [Parameter]
        public List<T> Items { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public T SelectedItem { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<T> SelectedItemChanged { get; set; }

        public SfAutoComplete<string, T> SfAutoComplete { get; set; }

        public async Task SetValue(string value)
        {
            this.Value = value;
            await ValueChanged.InvokeAsync(value);
        }

        public async Task SetSelectedItem(T item)
        {
            this.SelectedItem = item;
            await SelectedItemChanged.InvokeAsync(item);
        }

        public async Task OnValueChanged(
            ChangeEventArgs<string, T> changeEventArgs)
        {
            await SetValue(changeEventArgs.Value);
        }

        public async Task OnValueSelected(
            SelectEventArgs<T> selectEventArgs)
        {
            await SetSelectedItem(selectEventArgs.ItemData);
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

        private async Task OnFilter(FilteringEventArgs args)
        {
            args.PreventDefaultAction = true;
            var orPredicateForQueryFilter = new List<WhereFilter>();

            orPredicateForQueryFilter.Add(new WhereFilter()
            {
                Field = "Name",
                Operator = "contains",
                value = args.Text,
                IgnoreCase = true
            });

            WhereFilter orQueryFilter = WhereFilter.Or(orPredicateForQueryFilter);
            var query = new Query().Where(orQueryFilter);
            query = String.IsNullOrEmpty(args.Text) == false ? query : new Query();
            await SfAutoComplete.Filter(Items, query);
        }
    }
}
