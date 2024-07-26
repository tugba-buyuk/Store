using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryManager : ICountryService
    {
        private readonly IRepositoryManager _manager;

        public CountryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Country> GetAllCountries(bool trackChanges)
        {
            return _manager.Country.FindAll(trackChanges);
        }
    }
}
