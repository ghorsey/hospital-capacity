namespace Gah.HC.Spa.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.DomainBus;
    using Gah.HC.Commands;
    using Gah.HC.Domain;
    using Gah.HC.Queries;
    using Gah.HC.Spa.Authorization;
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
    [Route("api/hospitals")]
    [ApiController]
    [Authorize]
    public class HospitalsController : BaseController
    {
        private readonly IDomainBus domainBus;
        private readonly IAuthorizationService authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalsController" /> class.
        /// </summary>
        /// <param name="authorizationService">The authorization service.</param>
        /// <param name="domainBus">The domain bus.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// domainBus
        /// or
        /// authorizationService.
        /// </exception>
        public HospitalsController(IAuthorizationService authorizationService, IDomainBus domainBus, ILogger<HospitalsController> logger)
            : base(logger)
        {
            this.domainBus = domainBus ?? throw new ArgumentNullException(nameof(domainBus));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
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
            var authResult = await this.authorizationService.AuthorizeAsync(this.User, input, new CreateHospitalRequirement());

            if (!authResult.Succeeded)
            {
                return this.Forbid();
            }

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

            var q = this.MakeFindByIdOrSlugQuery(idOrSlug);

            var result = await this.domainBus.ExecuteAsync(q, cancellationToken);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result.MakeSuccessfulResult());
        }

        /// <summary>
        /// Gets the last recent capacity.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="maxRecentRecords">The maximum recent records.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{idOrSlug}/recent-capacity")]
        [ProducesResponseType(typeof(List<HospitalCapacityDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetLastRecentCapacity(
            string idOrSlug,
            CancellationToken cancellationToken,
            [FromQuery] int maxRecentRecords = 30)
        {
            this.Logger.LogInformation($"Finding the last {maxRecentRecords} recent capacity records for hospital {idOrSlug}");

            var hospitalQuery = this.MakeFindByIdOrSlugQuery(idOrSlug);
            var hospital = await this.domainBus.ExecuteAsync(hospitalQuery, cancellationToken);

            if (hospital == null)
            {
                return this.NotFound();
            }

            var query = new GetLastHospitalCapacityQuery(hospital.Id, maxRecentRecords, this.HttpContext.TraceIdentifier);

            var result = await this.domainBus.ExecuteAsync(query, cancellationToken);

            return this.Ok(result.Select(r => new HospitalCapacityDto
            {
                BedCapacity = r.BedCapacity,
                BedsInUse = r.BedsInUse,
                CreatedOn = r.CreatedOn,
                HospitalId = r.HospitalId,
                PercentOfUsage = r.PercentOfUsage,
            }).ToList());
        }

        /// <summary>
        /// Updates the hospital.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{idOrSlug}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateHospital(string idOrSlug, UpdateHospitalInput input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return this.BadRequest("Input cannot be null".MakeUnsuccessfulResult());
            }

            this.Logger.LogInformation($"Updating hosptial {idOrSlug}");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var hospital = await this.domainBus.ExecuteAsync(this.MakeFindByIdOrSlugQuery(idOrSlug), cancellationToken);

            // TODO: use auto mapper for this mappings...
            hospital.Name = input.Name;
            hospital.Address1 = input.Address1;
            hospital.Address2 = input.Address2;
            hospital.City = input.City;
            hospital.State = input.State;
            hospital.PostalCode = input.PostalCode;
            hospital.Phone = input.Phone;
            hospital.BedCapacity = input.BedCapacity;
            hospital.BedsInUse = input.BedsInUse;
            hospital.IsCovid = input.IsCovid;

            var authResult = await this.authorizationService.AuthorizeAsync(this.User, hospital, new UpdateHospitalRequirement());

            if (!authResult.Succeeded)
            {
                return this.Forbid();
            }

            var command = new UpdateHospitalCommand(hospital, this.HttpContext.TraceIdentifier);

            await this.domainBus.ExecuteAsync(command, cancellationToken);

            return this.NoContent();
        }

        /// <summary>
        /// rapid update as an asynchronous operation.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("{idOrSlug}/rapid-update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RapidUpdateAsync(string idOrSlug, RapidHospitalUpdateInput input, CancellationToken cancellationToken)
        {
            var authResult = await this.authorizationService.AuthorizeAsync(this.User, input, new CreateHospitalRequirement());
            if (!authResult.Succeeded)
            {
                return this.Forbid();
            }

            if (input == null)
            {
                return this.BadRequest("Input cannot be null".MakeUnsuccessfulResult());
            }

            this.Logger.LogInformation($"Rapid updating {idOrSlug} with capacity {input.BedCapacity} and usage {input.BedsInUse} and isCovid {input.IsCovid}");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.MakeUnsuccessfulResult());
            }

            var q = this.MakeFindByIdOrSlugQuery(idOrSlug);

            var hospital = await this.domainBus.ExecuteAsync(q, cancellationToken);

            var cmd = new RapidHospitalUpdateCommand(hospital.Id, input.BedsInUse, input.BedCapacity, input.IsCovid);

            await this.domainBus.ExecuteAsync(cmd, cancellationToken);

            return this.NoContent();
        }

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
        [ProducesResponseType(typeof(List<HospitalView>), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Makes the find by identifier or slug query.
        /// </summary>
        /// <param name="idOrSlug">The identifier or slug.</param>
        /// <returns>Gah.HC.Queries.FindBySlugOrIdQuery.</returns>
        private FindHospitalBySlugOrIdQuery MakeFindByIdOrSlugQuery(string idOrSlug)
        {
            FindHospitalBySlugOrIdQuery q;

            if (Guid.TryParse(idOrSlug, out var id))
            {
                q = new FindHospitalBySlugOrIdQuery(this.HttpContext.TraceIdentifier, id: id);
            }
            else
            {
                q = new FindHospitalBySlugOrIdQuery(this.HttpContext.TraceIdentifier, slug: idOrSlug);
            }

            return q;
        }
    }
}