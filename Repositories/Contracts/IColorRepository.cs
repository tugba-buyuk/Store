using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IColorRepository : IRepositoryBase<Color>
    {
        void CreateOneColor(Color color);
        void UpdateOneColor(Color color);
        void DeleteOneColor(Color color);
        public Color? GetOneColor(int id, bool trackChanges);
        List<Color> GetByIds(IEnumerable<int> ids);
    }
}
