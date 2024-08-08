
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.AppContext;
using Persistence.Implementaion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceRegistration
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddScoped(typeof(IAsyncBaseRepository<,,>), typeof(AsyncBaseRepository<,,>));
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();

            return services;
        }
    }
}
