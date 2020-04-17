using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.OpenApi.Expressions;
using TestWebApi.Controllers;
using TestWebApi.Models;
using TestWebApi.Patch;

namespace TestWebApi
{
    public class EnableRequestRewindMiddleware
    {
        private readonly RequestDelegate _next;

        public EnableRequestRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "PATCH")
            {
                context.Request.EnableBuffering();
            }

            await _next(context);
        }
    }

    public class TestAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method != "PATCH")
            {
                await next();
            }

            foreach (var pair in context.ActionArguments)
            {
                if (pair.Value is IPatch patch)
                {
                    using var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, false, 4096, leaveOpen: true);
                    context.HttpContext.Request.Body.Position = 0;
                    var body = await reader.ReadToEndAsync();
                    if (body.Length != 0)
                    {
                        patch.Raw = body;
                    }
                }
            }

            await next();
        }
    }
}