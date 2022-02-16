using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cinerva.Web.MiddleWare
{
    public interface ILogMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}
