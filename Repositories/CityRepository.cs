using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        private readonly RepositoryContext _context;
        public CityRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
    }
}
