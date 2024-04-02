using PostsService.Core.Middlewares;

namespace PostsService.Core
{
    public static class CoreAppCollections
    {
        public static IApplicationBuilder UseCoreApp(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();



            //app.UseSerilogRequestLogging(options =>
            //{
            //    // Customize the message template   
            //    options.MessageTemplate = "Request {RequestMethod} {RequestHost}{RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms - From {RemoteIpAddress}";

            //    // Emit debug-level events instead of the defaults
            //    //options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

            //    //Attach additional properties to the request completion event
            //    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            //    {
            //        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            //        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            //        diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
            //    };
            //});


            return app;
        }

    }
}
