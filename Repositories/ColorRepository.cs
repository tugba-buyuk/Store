using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        private readonly RepositoryContext _context;
        public ColorRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public void CreateOneColor(Color color)=>Create(color);

        public void DeleteOneColor(Color color) => Remove(color);

        public List<Color> GetByIds(IEnumerable<int> ids)
        {
            return _context.Colors
                .Where(c => ids.Contains(c.ColorId))
                .ToList();
        }

        public Color? GetOneColor(int id, bool trackChanges)
        {
            return FindByCondition(p => p.ColorId.Equals(id), trackChanges);
        }

        public void UpdateOneColor(Color color)
        {
            throw new NotImplementedException();
        }

       
    }
}
