namespace Gah.HC.Repository.Sql
{
    /// <summary>
    /// Interface IHospitalCapacityUow.
    /// </summary>
    public interface IHospitalCapacityUow
    {
        /// <summary>
        /// Gets the hospital repository.
        /// </summary>
        /// <value>The hospital repository.</value>
        IHospitalRepository HospitalRepository { get; }

        /// <summary>
        /// Gets the application user repository.
        /// </summary>
        /// <value>The application user repository.</value>
        IAppUserRepository AppUserRepository { get; }
    }
}
