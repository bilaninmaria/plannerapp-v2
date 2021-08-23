namespace PlannerApp.Shared.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    //The same object, but with payload (was a value).
    public class ApiResponse<T> : ApiResponse
    {
        public T Value { get; set; }
    }
}
