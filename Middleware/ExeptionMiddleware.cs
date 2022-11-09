using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExeptionMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger<ExeptionMiddleware> Logger { get; }
        public IHostEnvironment Env { get; }
        public ExeptionMiddleware(RequestDelegate next,ILogger<ExeptionMiddleware> logger,IHostEnvironment env)
        {
            this.Env = env;
            this.Logger = logger;
            this.Next = next;           
        }

        public async Task InvokeAsync(HttpContext context){
             try{
                  await Next(context);
             }catch(Exception ex){
                Logger.LogError(ex,ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode=500;

                var response= new ProblemDetails{
                    Status=500,
                    Detail=Env.IsDevelopment()?ex.StackTrace?.ToString():null,
                    Title=ex.Message
                };              
                var json=JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
             }            
        }
    }
}