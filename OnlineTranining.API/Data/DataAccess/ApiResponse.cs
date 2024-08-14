using System.Diagnostics;
using System.Net;

namespace OnlineTranining.API.Data.DataAccess
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object? Result { get; set; }
        public ICollection<string> ErrorMessages { get; }

        public ApiResponse()
        {
              ErrorMessages = new List<string>();
        }
    }
}
