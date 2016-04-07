namespace PaddleBuddy.Core.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Detail { get; set; }

        public Response()
        {
            Success = false;
            Detail = "Not set yet!";
        }
    }
}
