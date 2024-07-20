using Entities.Dtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IColorService
    {
        IEnumerable<Color> GetAllColors(bool trackChanges);
        Color? GetOneColor(int id, bool trackChanges);
        void CreateColor(ColorDtoForCreate colorDto);
        ColorDtoForUpdate GetOneColorForUpdate(int id, bool trackChanges);
        void UpdateOneColor(ColorDtoForUpdate colorDto);
        void DeleteOneColor(int id);
        List<Color> GetByIds(IEnumerable<int> ids);
    }
}
