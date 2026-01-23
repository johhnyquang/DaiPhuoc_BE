namespace DaiPhuocBE.DTOs
{
    public class APIResponse <T>
    {
        public bool Success { get; set; }
        public string Apiversion { get; set; } = "V1";
        public string Message { get; set; }
        public T Data { get; set; }

        public APIResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data!;
            Apiversion = Apiversion;
        }
    }
}
