using System.Collections.Generic;

namespace NetCoreLibrary.Web.Models
{
    /// <summary>
    /// ApiController için modelstate invalid olduðunda döneceðim response model
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
