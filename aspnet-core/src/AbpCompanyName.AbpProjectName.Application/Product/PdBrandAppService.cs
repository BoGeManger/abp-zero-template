using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using AbpCompanyName.AbpProjectName.Dto;
using AbpCompanyName.AbpProjectName.Entities;
using AbpCompanyName.AbpProjectName.Authorization;

namespace AbpCompanyName.AbpProjectName.Service
{
    [AbpAuthorize(PermissionNames.Pages_PdBrand)]
    public class PdBrandAppService : AbpProjectNameAppServiceBase, IPdBrandAppService
    {
        private readonly IRepository<PdBrand, int> _pdbrandRepository;

        public PdBrandAppService(IRepository<PdBrand, int> pdbrandRepository)
        {
            _pdbrandRepository = pdbrandRepository;
        }

        /// <summary>
        /// 获取分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PdBrandListDto>> GetPaged(PdBrandGetPagedIn input)
        {
            var query = _pdbrandRepository.GetAll().WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                a => a.BrandName.Contains(input.Filter));
            var pdbrandCount = await query.CountAsync();
            var pdbrandListDtos = new List<PdBrandListDto>();
            if (pdbrandCount > 0)
            {
                var pdbrands = await query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToListAsync();
                pdbrandListDtos = pdbrands.MapTo<List<PdBrandListDto>>();
            }
            return new PagedResultDto<PdBrandListDto>(pdbrandCount, pdbrandListDtos);
        }

        /// <summary>
        /// 通过指定id获取信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PdBrandListDto> GetById(EntityDto<int> input)
        {
            var entity = await _pdbrandRepository.GetAsync(input.Id);
            return entity.MapTo<PdBrandListDto>();
        }

        /// <summary>
        /// 添加或者修改的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(PdBrandCreateOrUpdateIn input)
        {
            if (input.PdBrand.Id.HasValue)
                await UpdateAsync(input.PdBrand);
            else
                await CreateAsync(input.PdBrand);
        }

        /// <summary>
        /// 删除信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>        
        public async Task Delete(EntityDto<int> input)
        {
            await _pdbrandRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task BatchDelete(List<int> input)
        {
            await _pdbrandRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task<PdBrandEditDto> CreateAsync(PdBrandEditDto input)
        {
            var entity = ObjectMapper.Map<PdBrand>(input);
            entity = await _pdbrandRepository.InsertAsync(entity);
            return entity.MapTo<PdBrandEditDto>();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task UpdateAsync(PdBrandEditDto input)
        {
            var entity = await _pdbrandRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            await _pdbrandRepository.UpdateAsync(entity);
        }
    }
}
