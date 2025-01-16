using PavoWeb.Database;
using PavoWeb.Handler;
using PavoWeb.Repository;
using Serilog;


namespace PavoWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                         .WriteTo.Console()  
                         .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)  
                         .CreateLogger();
            try
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()  // Allows any origin
                        .AllowAnyMethod()      // Allows any HTTP method (GET, POST, PUT, DELETE)
                        .AllowAnyHeader());    // Allows any header
            });
            //builder.Services.AddSingleton(builder.Configuration.GetConnectionString("DefaultConnection"));
            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                var builder = WebApplication.CreateBuilder(args);
                
                builder.Logging.ClearProviders();  
                builder.Logging.AddSerilog();  

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder =>
                        builder.AllowAnyOrigin()  
                            .AllowAnyMethod()      
                            .AllowAnyHeader());    
                });

                builder.Services.AddSingleton<DBConnection>();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddControllers();
                builder.Services.AddOpenApi();
                builder.Services.AddScoped<MenuHandler>();
                builder.Services.AddScoped<MenuRepository>();
                builder.Services.AddScoped<ContentHandler>();
                builder.Services.AddScoped<ContentRepository>();
                builder.Services.AddScoped<SocialIconHandler>();
                builder.Services.AddScoped<SocialIconRepository>();
                builder.Services.AddScoped<StatisticsHandler>();
                builder.Services.AddScoped<StatisticsRepository>();
                builder.Services.AddScoped<SubscriptionHandler>();
                builder.Services.AddScoped<SubscriptionRepository>();

                var app = builder.Build();


                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                        options.RoutePrefix = string.Empty; 
                    });
                }

                app.UseCors("AllowAll");
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush(); 
            }
        }
    }
}
