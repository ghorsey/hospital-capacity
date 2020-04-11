namespace Gah.HC.Spa.MappingConfig
{
    using AutoMapper;
    using Gah.HC.Domain;
    using Gah.HC.Spa.Models.Shared;

    /// <summary>
    /// Class UserDtoMapping.
    /// Implements the <see cref="AutoMapper.Profile" />.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class UserDtoMapping : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDtoMapping"/> class.
        /// </summary>
        public UserDtoMapping()
        {
            this.CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
