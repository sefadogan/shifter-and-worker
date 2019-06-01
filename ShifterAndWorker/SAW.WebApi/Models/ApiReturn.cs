using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.WebApi
{
    public class ApiReturn
    {
        public const short ERROR_CODE_NO_ERROR = 0;
        public const short ERROR_CODE_UNAUTHORIZED = 401;
        public const short ERROR_CODE_NOT_FOUND = 404;
        public const short ERROR_CODE_INVALID_OPERATION = 501;
        public const short ERROR_CODE_INTERNAL_ERROR = 500;

        public bool Success { get; set; }
        public string Message { get; set; }
        public short ErrorCode { get; set; }
        public object Data { get; set; }

        public static ApiReturn Successful(object data = null, string message = null)
        {
            return new ApiReturn
            {
                Success = true,
                Message = message ?? "Process successful.",
                ErrorCode = ERROR_CODE_NO_ERROR,
                Data = data
            };
        }
        public static ApiReturn Unauthorized(string message = null)
        {
            return new ApiReturn
            {
                Success = false,
                Message = message ?? "You have no permission to access.",
                ErrorCode = ERROR_CODE_UNAUTHORIZED,
                Data = null
            };
        }
        public static ApiReturn NotFound(string message = null)
        {
            return new ApiReturn
            {
                Success = false,
                Message = message ?? "Data is not found.",
                ErrorCode = ERROR_CODE_NOT_FOUND,
                Data = null
            };
        }
    }
}
