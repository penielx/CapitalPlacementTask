using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CapitalPlacement.Shared.Responses
{
    public class StandardResponse<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public StandardResponse(int statusCode, bool success, string msg, T data)
        {
            Data = data;
            Success = success;
            StatusCode = statusCode;
            Message = msg;
        }
        public StandardResponse()
        {
        }

        /// <summary>
        /// Application custom response message, 99 response means failed
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static StandardResponse<T> Failed(string errorMessage, int statusCode = 400)
        {
            return new StandardResponse<T> { Success = false, Message = errorMessage, StatusCode = statusCode };
        }

        public static StandardResponse<T> Failed(string errorMessage, T? data, int statusCode = 400)
        {
            return new StandardResponse<T> { Success = false, Message = errorMessage, Data = data, StatusCode = statusCode };
        }

        /// <summary>
        /// Application custom response message, 00 means successful
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static StandardResponse<T> Succeeded(string successMessage, T? data, int statusCode = 200)
        {
            return new StandardResponse<T> { Success = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }

        /// <summary>
        /// Application custom response message, 66 means third party error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static StandardResponse<T> UnExpectedError(string message, T data, int statusCode = 500)
        {
            return new StandardResponse<T> { Success = false, Message = message, Data = data, StatusCode = statusCode };
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

}
