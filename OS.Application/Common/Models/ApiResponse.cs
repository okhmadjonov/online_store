namespace OS.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Operatsiya muvaffaqiyatli bajarildi", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                Errors = null,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> Failure(string message, List<string>? errors = null, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<string> { message },
                StatusCode = statusCode
            };
        }
    }
}
