namespace Microservice_RatingService.Application.Models
{
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }

    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public List<ValidationError> Errors { get; set; }
        public string TraceId { get; set; }

        public ApiErrorResponse(string message = null)
        {
            Message = message ?? "One or more validation errors occurred.";
            Errors = new List<ValidationError>();
            TraceId = Guid.NewGuid().ToString();
        }
    }
}
