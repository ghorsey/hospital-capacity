namespace Gah.HC.Repository
{
    /// <summary>
    /// Interface IHospitalCapacityUow.
    /// </summary>
    public interface IHospitalCapacityUow : IUnitOfWork
    {
        /// <summary>
        /// Gets the hospital repository.
        /// </summary>
        /// <value>The hospital repository.</value>
        IHospitalRepository HospitalRepository { get; }

        /// <summary>
        /// Gets the hospital capacity repository.
        /// </summary>
        /// <value>The hospital capacity repository.</value>
        IHospitalCapacityRepository HospitalCapacityRepository { get; }

        /// <summary>
        /// Gets the region repository.
        /// </summary>
        /// <value>The region repository.</value>
        IRegionRepository RegionRepository { get; }
    }
}
