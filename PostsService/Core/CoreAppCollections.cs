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

            app.UseGlobalExceptions();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            return app;
        }

    }
}
