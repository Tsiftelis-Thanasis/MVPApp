using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVPApp.Context;
using MVPApp.Models;

namespace MVPApp
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Build configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // Use configuration in the app
            string environment = Configuration["Environment"];
            Console.WriteLine($"Environment: {environment}");

            ApplicationConfiguration.Initialize();

            // Set up dependency injection
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the Presenter and View (LoginForm)
            services.AddTransient<LoginPresenter>();
            //services.AddTransient<ILoginView, LoginForm>();  // Register LoginForm as ILoginView
            services.AddTransient<LoginForm>();
            services.AddTransient<RegisterPresenter>();
            services.AddTransient<RegisterForm>();
            services.AddTransient<DashboardForm>();

            using var serviceProvider = services.BuildServiceProvider();
            var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            loginForm.ServiceProvider = serviceProvider;
            Application.Run(loginForm);
        }
    }
}