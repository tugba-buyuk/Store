using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly RepositoryContext _context;
        public CategoryRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public void CreateOneCategory(Category category) => Create(category);
        public void UpdateOneCategory(Category category) => Update(category);

        public Category? GetOneCategory(int id, bool trackChanges)
        {
            return FindByCondition(p => p.Id.Equals(id), trackChanges);
        }

        public void DeleteOneCategory(Category category) => Remove(category);
        
    }
}
