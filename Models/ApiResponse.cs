namespace DemoAPI.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }



        public ApiResponse(string message, T data)
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = null;
        }

        public ApiResponse(List<string> errors, string message)
        {
            Success = false;
            Message = message;
            Errors = errors ?? new List<string>();
        }
    }
}
