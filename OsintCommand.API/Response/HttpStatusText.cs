namespace OsintCommand.API.Response
{
    public class HttpStatusText
    {
        public static string GetStatusText(int code)
        {
            return code switch
            {
                200 => "Success",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                409 => "Conflict",
                422 => "Unprocessable Entity",
                503 => "Service Unavailable",
                500 => "Internal Server Error",
                _ => "Unknown"
            };
        }
    }
}
