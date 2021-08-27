﻿using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services.Interfaces
{
    public interface IPlansService
    {
        Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string query = null, int pageNumber = 1, int pageSize = 10);
    }
}