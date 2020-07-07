using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace AppStock.Infrastructure.Extensions
{
    /// <summary>
    /// Lazy Resolution extensions
    /// </summary>
    public static class LazyResolution
    {
        /// <summary>
        /// Add lazy resolution
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLazyResolution(this IServiceCollection services)
        {
            return services.AddTransient(
                typeof(Lazy<>),
                typeof(LazilyResolved<>));
        }
 
        private class LazilyResolved<T> : Lazy<T>
        {
            public LazilyResolved(IServiceProvider serviceProvider)
                : base(serviceProvider.GetRequiredService<T>)
            {
            }
        }
    }
}