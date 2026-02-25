using AutoMapper;
using CBS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBS.BLL.Services
{
    public class Service<TEntity, TDto> : IService<TEntity,TDto> 
    where TEntity : class
    where TDto : class

    {

        private IGenericRepository<TEntity> Repository { get; set; }
        private IMapper Dtmapper { get; set; }

        public Service(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Dtmapper = mapper;
        }
        public async Task<IEnumerable<TDto>> All()
        {
            try
            {
                var entities = await Repository.GetAll();
                var dtos = Dtmapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entities);
                return await Task.FromResult(dtos);

            }
            catch (TimeoutException ext)
            {
                throw new ErrorService(ext, 504);
            }
            catch (Exception ex)
            {
                throw new ErrorService(ex, 500);
            }
        }

        public async Task<TDto> Create(TDto model)
        {
            try
            {
                var  entity = Dtmapper.Map<TDto, TEntity>(model);
                await Repository.Add(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw new ErrorService(ex, 500);

            }
        }

        public async Task<TDto> FindById(long id)
        {
            try
            {
                var entity = await Repository.GetById(id);
                if (entity == null)
                    throw new ErrorService(new Exception("Not found"), 400);

                var dtos = Dtmapper.Map<TEntity, TDto>(entity);
                return dtos;
            }
            catch (ErrorService exs)
            {
                throw exs;
            }
            catch (Exception ex)
            {
                throw new ErrorService(ex, 500);
            }
        }

        public async Task Modify(long id, TDto model)
        {
            try
            {
                var entity = await Repository.GetById(id);
                if (entity == null)
                    throw new ErrorService(new Exception("Not found"), 400);

                var entitytoupdate = Dtmapper.Map<TDto, TEntity>(model);

                await Repository.Update(entitytoupdate);
            }
            catch (ErrorService exs)
            {
                throw exs;
            }
            catch (Exception ex)
            {
                throw new ErrorService(ex, 500);
            }
        }
        
        public async Task<bool> Remove(long id)
        {
            try
            {
                var entity = await Repository.GetById(id);
                if (entity == null)
                    throw new ErrorService(new Exception("Not found"), 400);
                var result = await Repository.Delete(entity);
                return result;
            }
            catch (ErrorService exs)
            {
                throw exs;
            }
            catch (Exception ex)
            {
                throw new ErrorService(ex, 500);
            }
        }
    }
}
