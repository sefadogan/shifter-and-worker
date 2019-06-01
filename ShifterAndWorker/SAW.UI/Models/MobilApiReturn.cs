using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Models
{
    public class MobilApiReturn
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

        public static JsonResult Successful(string message = null)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    success = true,
                    message = message ?? "Process successful.",
                    errorCode = ERROR_CODE_NO_ERROR
                }
            };

            return jsonResult;
        }
        public static JsonResult Unauthorized(string message = null)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    success = false,
                    message = message ?? "You have no permission to access.",
                    errorCode = ERROR_CODE_UNAUTHORIZED,
                    data = string.Empty
                }
            };

            return jsonResult;
        }
        public static JsonResult NotFound(string message = null)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    success = false,
                    message = message ?? "Data is not found.",
                    errorCode = ERROR_CODE_NOT_FOUND,
                    data = string.Empty
                }
            };

            return jsonResult;
        }
        public static JsonResult InvalidOperation(string message = null)
        {
            JsonResult jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    success = false,
                    message = message ?? "An error occured while performing the operation.",
                    errorCode = ERROR_CODE_INVALID_OPERATION,
                    data = string.Empty
                }
            };

            return jsonResult;
        }
    }
}