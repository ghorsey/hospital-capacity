namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.EventBus;
    using Gah.HC.Commands;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Spa.Models.Hospitals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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
        private readonly IDomainBus domainBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalsController" /> class.
        /// </summary>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        public HospitalsController(IDomainBus domainBus, ILogger<HospitalsController> logger)
            : base(logger)
        {
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
        }

        /// <summary>
        /// create hospital as an asynchronous operation.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Hospital), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateHospitalAsync([FromBody] CreateHospitalInput input, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Creating a hospital");

            if (input == null)
            {
                return this.BadRequest("input cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var hospital = new Hospital
            {
                Address1 = input.Address1,
                Address2 = input.Address2,
                BedCapacity = input.BedCapacity,
                BedsInUse = input.BedsInUse,
                City = input.City,
                IsCovid = input.IsCovid,
                Name = input.Name,
                PostalCode = input.PostalCode,
                RegionId = input.RegionId,
                State = input.State,
                Phone = input.Phone,
            };
            var command = new CreateHospitalCommand(hospital, this.HttpContext.TraceIdentifier);

            await this.domainBus.ExecuteAsync(command, cancellationToken);

            return this.Ok(hospital.MakeSuccessfulResult());
        }

        /// <summary>
        /// Gets the hospital.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{idOrSlug}")]
        [ProducesResponseType(typeof(Hospital), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetHospitalAsync(string idOrSlug, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Finding hospital by id or slug: {idOrSlug}");

            FindBySlugOrIdQuery q;

            if (Guid.TryParse(idOrSlug, out var id))
            {
                q = new FindBySlugOrIdQuery(this.HttpContext.TraceIdentifier, id: id);
            }
            else
            {
                q = new FindBySlugOrIdQuery(this.HttpContext.TraceIdentifier, slug: idOrSlug);
            }

            var result = await this.domainBus.ExecuteAsync(q, cancellationToken);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result.MakeSuccessfulResult());
        }

        /////// <summary>
        /////// rapid update as an asynchronous operation.
        /////// </summary>
        /////// <param name="input">The input.</param>
        /////// <param name="cancellationToken">The cancellation token.</param>
        /////// <returns>Task&lt;IActionResult&gt;.</returns>
        ////[HttpPost("{idOrSlug}/rapid-update")]
        ////[ProducesResponseType(StatusCodes.Status200OK)]
        ////public async Task<IActionResult> RapidUpdateAsync(RapidHospitalUpdateInput input, CancellationToken cancellationToken)
        ////{

        ////    // todo: need to implement.
        ////    return await Task.FromResult(this.NoContent());
        ////}

        /// <summary>
        /// get hospitals as an asynchronous operation.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="bedCapacity">The bed capacity.</param>
        /// <param name="bedsInUse">The beds in use.</param>
        /// <param name="percentOfUse">The percentage available.</param>
        /// <param name="isCovid">if set to <c>true</c> [is covid].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Hospital>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetHospitalsAsync(
            [FromQuery] Guid? regionId = null,
            [FromQuery] string name = "",
            [FromQuery] string city = "",
            [FromQuery] string state = "",
            [FromQuery] string postalCode = "",
            [FromQuery] int bedCapacity = 0,
            [FromQuery] int bedsInUse = -1,
            [FromQuery] int percentOfUse = -1,
            [FromQuery] bool? isCovid = null,
            CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation("Searching for hospitals");

            var q = new FindHospitalsQuery(
                this.HttpContext.TraceIdentifier,
                regionId,
                name,
                city,
                state,
                postalCode,
                bedCapacity,
                bedsInUse,
                percentOfUse,
                isCovid);

            var result = await this.domainBus.ExecuteAsync(q, cancellationToken);

            return this.Ok(result.MakeSuccessfulResult());
        }
    }
}