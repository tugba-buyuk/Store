using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICityService
    {
        IEnumerable<City> GetAllCities(bool trackChanges);
    }
}
