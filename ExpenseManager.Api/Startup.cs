using ExpenseManager.Api.Database;
using ExpenseManager.Api.Service;
using ExpenseManager.Api.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Api
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpenseManagerDBContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();

            RegisterRepositories.Register(services);
        }
    }
}
