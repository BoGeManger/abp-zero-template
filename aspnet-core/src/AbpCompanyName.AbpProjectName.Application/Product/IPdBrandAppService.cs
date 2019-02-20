using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbpCompanyName.AbpProjectName.Dto;
using Abp.Application.Services;
using Abp.Application.Services.Dto;

namespace AbpCompanyName.AbpProjectName.Service
{
    public interface IPdBrandAppService:IApplicationService
    {
        /// <summary>
        /// 获取PdBrand分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PdBrandListDto>> GetPaged(PdBrandGetPagedIn input);

        /// <summary>
        /// 通过指定id获取PdBrand信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PdBrandListDto> GetById(EntityDto<int> input);

        /// <summary>
        /// 添加或者修改PdBrand的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(PdBrandCreateOrUpdateIn input);

        /// <summary>
        /// 删除PdBrand信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>       
        Task Delete(EntityDto<int> input);

        /// <summary>
        /// 批量删除PdBrand的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task BatchDelete(List<int> input);
    }
}
