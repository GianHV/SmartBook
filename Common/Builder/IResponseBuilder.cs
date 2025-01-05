using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Base;

namespace Common.Builder
{
    public interface IResponseBuilder
    {
        ApiResponse Build<T>(T obj, HttpStatusCode status);
    }
}
