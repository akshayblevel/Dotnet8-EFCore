using Dotnet8_EFCore.Interfaces;
using Dotnet8_EFCore.Models;

namespace Dotnet8_EFCore.DataAccess
{
    public class LocationDb : ILocationDb
    {
        private readonly LocationContext _locationContext;

        private IRepository<Country> _countryRepository;
        private IRepository<State> _stateRepository;
        private IRepository<City> _cityRepository;
        public LocationDb(LocationContext context)
        {
            _locationContext = context;
        }

        public IRepository<Country> CountryRepository => _countryRepository ??= new Repository<Country>(_locationContext);

        public IRepository<State> StateRepository => _stateRepository ??= new Repository<State>(_locationContext);

        public IRepository<City> CityRepository => _cityRepository ??= new Repository<City>(_locationContext);
    }
}
