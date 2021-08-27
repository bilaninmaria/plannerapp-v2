using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace PlannerApp.V2.Components
{/// <summary>
/// This class will be responsible just for the UI. All the logic will be in the parent class PlansList.
/// </summary>
    public partial class PlanCardsList
    {
        public bool _isBusy { get; set; }
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private string _query = string.Empty;

        //We will pass GetPlansAsync as a delegate function in here, this class is the child component.
        //This propertie is going to be a delegate of type Func(Func is a data type or a variable that represents a function).
        //A delegate needs a function but its not responsible for its implementation(delegate means that implement for someone else and its just takes the signature)
        [Parameter]
        public Func<string, int, int, Task<PagedList<PlanSummary>>> FetchPlans { get; set; }

        private PagedList<PlanSummary> _result = new();

        protected async override Task OnInitializedAsync()
        {
            _isBusy = true;
            _result = await FetchPlans?.Invoke(_query, _pageNumber, _pageSize);
            _isBusy = false;
        }
    }
}
