namespace OsintCommand.API.Response
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("o");

        public ApiResponse(int code, string message, T data)
        {
            Code = code;
            Status = HttpStatusText.GetStatusText(code);
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Ok(T data, string message = "Success")
            => new ApiResponse<T>(200, message, data);

        public static ApiResponse<T> NoContent(string message = "No Content")
            => new ApiResponse<T>(204, message, default);

        public static ApiResponse<T> Fail(int code, string message)
            => new ApiResponse<T>(code, message, default);
    }
}
