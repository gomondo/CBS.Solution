
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CBS.BLL.Services
{
    public class ErrorService : Exception
    {
        public string ErrorMessage { get; set; } = "Ok";
        public int ErrorCode { get; set; } = 200;
        public Type SourceType { get; set; }
        public ErrorService(Exception ex, int errorCode) : base(ex.Message,ex.InnerException)
        {
            if (errorCode != 0)
            {
                ErrorCode = errorCode;

                if (errorCode == 500)
                    ErrorMessage = "Internal Server Error-Database or backend implementation error please report issue admin@cbs.com";
                else if (errorCode == 501)
                    ErrorMessage = "Not Implemented the features are currently unavailable.";
                else if (errorCode == 502)
                    ErrorMessage = "Network or provider unavailable";
                else if (errorCode == 503)
                    ErrorMessage = "Service or resource unavailable Unavailable";
                else if (errorCode == 504)
                    ErrorMessage = "Timeout issue please try  later the issue has been load and our team is looking at it.";
                else if (errorCode == 404)
                    ErrorMessage = "The record was not found in the Database";
                else if (errorCode == 400)
                    ErrorMessage = "Bad request check the format of your data or any missing required data";
            }
        }
    }
}
 