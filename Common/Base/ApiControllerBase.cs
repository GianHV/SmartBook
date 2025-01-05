using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Common.Base
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly IResponseBuilder _responseBuilder;
        public ApiControllerBase()
        {
            _responseBuilder = new ResponseBuilder();
        }
        protected IActionResult APIResponse<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse res = _responseBuilder.Build(value, statusCode);

            return StatusCode((int)res.StatusCode, res);
        }
    }
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object? Result { get; set; }
    }
}
