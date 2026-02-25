using System;
using System.Collections.Generic;
using System.Text;

namespace CBS.BLL.Services
{
    public interface IService<TEntity,TDto>
    {
        Task<IEnumerable<TDto>> All();
        Task<TDto> FindById(long id);
        Task<TDto> Create(TDto model);
        Task Modify(long id, TDto model);
        Task<bool>Remove(long id);      
    }
}
