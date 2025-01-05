using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Base;

namespace Common.Builder
{
    public class ResponseBuilder : IResponseBuilder
    {
        public ApiResponse Build<T>(T obj, HttpStatusCode status = HttpStatusCode.OK)
        {
            ApiResponse response = new ApiResponse();
            switch (status)
            {
                case HttpStatusCode.BadRequest:
                    response.StatusCode = status;
                    response.IsSuccess = false;
                    response.Result = null;
                    response.ErrorMessages = new List<string>() { "Please try again later" };
                    break;
                case HttpStatusCode.Unauthorized:
                    response.StatusCode = status;
                    response.IsSuccess = false;
                    response.Result = null;
                    response.ErrorMessages = new List<string>() { "Your email or password is not correct" };
                    break;
                case HttpStatusCode.Forbidden:
                    response.StatusCode = status;
                    response.IsSuccess = false;
                    response.Result = null;
                    response.ErrorMessages = new List<string>()
                        { "You do not have permission to use this service" };
                    break;
                default:
                    response.IsSuccess = true;
                    response.Result = obj;
                    break;
            }

            return response;

        }
    }
}
