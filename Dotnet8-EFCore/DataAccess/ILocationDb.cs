using Dotnet8_EFCore.Interfaces;
using Dotnet8_EFCore.Models;

namespace Dotnet8_EFCore.DataAccess
{
    public interface ILocationDb
    {
        IRepository<Country> CountryRepository { get; }
        IRepository<State> StateRepository { get; }
        IRepository<City> CityRepository { get; }
    }
}
