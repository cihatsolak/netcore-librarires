using System.Collections.Generic;

namespace NetCoreLibrary.Web.Models
{
    /// <summary>
    /// ApiController i�in modelstate invalid oldu�unda d�nece�im response model
    /// </summary>
    public class ErrorResponseModel
    {
        public ErrorResponseModel()
        {
            Errors = new List<string>();
        }

        public int Status { get; set; }
        public List<string> Errors { get; set; }
    }
}
