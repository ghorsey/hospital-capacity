namespace Gah.HC.Repository
{
    using System;
    using Gah.HC.Domain;

    /// <summary>
    /// Interface IHospitalRepository
    /// Implements the <see cref="Gah.HC.Repository.IRepository{Hospital, Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.IRepository{Hospital, Guid}" />
    public interface IHospitalRepository : IRepository<Hospital, Guid>
    {
    }
}
