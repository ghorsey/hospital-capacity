namespace Gah.HC.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.HC.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Generic Repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class RepositoryBase<TEntity, TId, TDbContext> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TDbContext : DbContext
    {
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity, TId, TDbContext}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public RepositoryBase(TDbContext context, ILogger<RepositoryBase<TEntity, TId, TDbContext>> logger)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            this.Logger = logger;
            this.Context = context;
            this.Entities = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected TDbContext Context { get; }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        protected DbSet<TEntity> Entities { get; }

        /// <summary>
        /// Finds the specified entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <typeparamref name="TEntity" />.</returns>
        public virtual ValueTask<TEntity> FindAsync(TId id, CancellationToken cancellationToken = default)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            this.Logger.LogInformation($"Searching by ID: {id}");
            return this.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task&lt;TEntity&gt;.</returns>
        public virtual ValueTask<TEntity> FindAsync(object[] ids, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Searching by ID: {@ids}", ids);
            return this.Entities.FindAsync(ids, cancellationToken);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity - Entity is required.</exception>
        /// <inheritdoc />
        public virtual Task AddAsync(TEntity entity)
        {
            this.Logger.LogInformation($"Adding a new entity.");
            if (entity == null)
            {
                this.Logger.LogWarning("Entity is null");
                throw new ArgumentNullException(nameof(entity), "Entity is required.");
            }

            this.Logger.LogDebug($"Attempting to add entity({entity.Id}).");

            this.Entities.Add(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Adds the range asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>Task.</returns>
        public Task AddRangeAsync(params TEntity[] entities)
        {
            entities = entities ?? throw new ArgumentNullException(nameof(entities));
            this.Logger.LogInformation("Adding a range of {count} entities", entities.Length);
            return this.AddRangeAsync(entities.AsEnumerable());
        }

        /// <summary>
        /// Adds the range asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>Task.</returns>
        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            this.Logger.LogInformation("Adding a range of {count} entities", entities.Count());
            return this.Entities.AddRangeAsync(entities);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity - Entity is required.</exception>
        /// <inheritdoc />
        public virtual Task UpdateAsync(TEntity entity)
        {
            this.Logger.LogInformation($"Updating an entity.");
            if (entity == null)
            {
                this.Logger.LogWarning($"Entity is null.");
                throw new ArgumentNullException(nameof(entity), "Entity is required.");
            }

            this.Logger.LogDebug($"Attempting to update entity({entity.Id})");
            this.Entities.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates the range asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>Task.</returns>
        public Task UpdateRangeAsync(params TEntity[] entities)
        {
            this.Logger.LogTrace($"Calling {nameof(this.UpdateRangeAsync)} with params, forwarding...");
            return this.UpdateRangeAsync(entities.AsEnumerable());
        }

        /// <summary>
        /// Updates the range asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>Task.</returns>
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            this.Logger.LogInformation("Updating {count} entities", entities.Count());
            this.Entities.UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity - Entity is required.</exception>
        /// <inheritdoc />
        public virtual Task RemoveAsync(TEntity entity)
        {
            this.Logger.LogInformation($"Deleting an entity.");
            if (entity == null)
            {
                this.Logger.LogWarning($"Entity is null.");
                throw new ArgumentNullException(nameof(entity), "Entity is required.");
            }

            this.Logger.LogDebug($"Attempting to remove entity({entity.Id}).");
            this.Entities.Remove(entity);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Removes the range asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>Task.</returns>
        public Task RemoveRangeAsync(params TEntity[] entities)
        {
            this.Logger.LogTrace($"Called {nameof(this.RemoveRangeAsync)} with params, forwarding...");
            return this.RemoveRangeAsync(entities.AsEnumerable());
        }

        /// <summary>
        /// Removes the range asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>Task.</returns>
        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            this.Logger.LogInformation("Removing {count} entities", entities.Count());
            this.Entities.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
