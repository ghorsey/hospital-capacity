namespace Gah.HC.Spa.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class HospitalsController.
    /// Implements the <see cref="Gah.HC.Spa.Controllers.BaseController" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Spa.Controllers.BaseController" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HospitalsController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HospitalsController(ILogger<HospitalsController> logger)
            : base(logger)
        {
        }

        ////[HttpPost]
        ////[ProducesResponseType(typeof(Hospital), StatusCodes.Status200OK)]
        ////Neeed to create endpoint to save record, a region endpoint to find by slug
    }
}