using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        private readonly RepositoryContext _context;
        public CountryRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
    }
}
