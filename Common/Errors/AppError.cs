using System;
using Common.Enums;

namespace Common.Errors
{
    public class AppError: Exception
    {
        /// <summary>
        /// Internal Code
        /// </summary>
        public int ErrorCode { get; set; } = (int)ErrorCodeBase.AppError;

        public AppError()
        {
        }

        public AppError(string message) : base(message)
        {
        }

        public AppError(string message, int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
