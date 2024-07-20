using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ColorManager : IColorService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ColorManager(IMapper mapper, IRepositoryManager manager)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateColor(ColorDtoForCreate colorDto)
        {
            Color color=_mapper.Map<Color>(colorDto);
            _manager.Color.CreateOneColor(color);
            _manager.Save();
        }

        public void DeleteOneColor(int id)
        {
            var color = _manager.Color.GetOneColor(id, false);
            if(color is null)
            {
                throw new Exception("The color is not found!");
            }
            _manager.Color.DeleteOneColor(color);
            _manager.Save();
        }

        public IEnumerable<Color> GetAllColors(bool trackChanges)
        {
            return _manager.Color.FindAll(trackChanges);
        }

        public Color? GetOneColor(int id, bool trackChanges)
        {
            var color = _manager.Color.GetOneColor(id, trackChanges);
            if(color is null)
            {
                throw new Exception("The color is not found");
            }
            return color;
        }

        public ColorDtoForUpdate GetOneColorForUpdate(int id, bool trackChanges)
        {
            var color = _manager.Color.GetOneColor(id, false);
            var colorDto = _mapper.Map<ColorDtoForUpdate>(color);
            return colorDto;
        }

        public void UpdateOneColor(ColorDtoForUpdate colorDto)
        {
            var entity=_mapper.Map<Color>(colorDto);
            _manager.Color.UpdateOneColor(entity);
            _manager.Save();
        }

        public List<Color> GetByIds(IEnumerable<int> ids)
        {
            return _manager.Color.GetByIds(ids);
        }
    }
}
