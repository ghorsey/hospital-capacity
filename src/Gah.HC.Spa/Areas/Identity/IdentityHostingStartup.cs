using System;
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Gah.HC.Spa.Areas.Identity.IdentityHostingStartup))]

namespace Gah.HC.Spa.Areas.Identity
{
    /// <summary>
    /// Class IdentityHostingStartup.
    /// Implements the <see cref="Microsoft.AspNetCore.Hosting.IHostingStartup" />.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Hosting.IHostingStartup" />
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// Configure the <see cref="Microsoft.AspNetCore.Hosting.IWebHostBuilder" />.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <remarks>Configure is intended to be called before user code, allowing a user to overwrite any changes made.</remarks>
        public void Configure(IWebHostBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}