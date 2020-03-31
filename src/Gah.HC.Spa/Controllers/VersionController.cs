namespace Gah.HC.Spa.Controllers
{
    using System.Diagnostics;
    using System.Net;
    using Gah.HC.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class VersionController.
    /// </summary>
    [ApiController]
    [Route("versionz")]
    public class VersionController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public VersionController(ILogger<VersionController> logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Versions this instance.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.OK)]
        public IActionResult Version()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            var version = fileVersion.FileVersion;

            this.Logger.LogInformation(1, "Fetching version: {versionString}", version);

            return this.Ok(version.MakeSuccessfulResult());
        }
    }
}
