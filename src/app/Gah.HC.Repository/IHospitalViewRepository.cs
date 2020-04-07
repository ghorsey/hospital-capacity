namespace Gah.HC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Gah.HC.Domain;

    /// <summary>
    /// Interface IHospitalViewRepository
    /// Implements the <see cref="Gah.HC.Repository.IRepository{HospitalView, Guid}" />.
    /// </summary>
    /// <seealso cref="Gah.HC.Repository.IRepository{HospitalView, Guid}" />
    public interface IHospitalViewRepository : IRepository<HospitalView, Guid>
    {
    }
}
