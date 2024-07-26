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
    public class CityManager : ICityService
    {
        private readonly IRepositoryManager _manager;

        public CityManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<City> GetAllCities(bool trackChanges)
        {
            return _manager.City.FindAll(trackChanges);
        }
    }
}
