using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Api.Repository;
using ExpenseManager.Api.Repository.Interfaces;
using ExpenseManager.Api.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service
{
    public class RegisterRepositories
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<PaymentType>, Repository<PaymentType>>();
            services.AddScoped<IRepository<DefaultExpenseCategory>, Repository<DefaultExpenseCategory>>();
            services.AddScoped<IRepository<UserExpenseCategory>, Repository<UserExpenseCategory>>();
            services.AddScoped<IRepository<Expense>, Repository<Expense>>();
        }
    }
}
