using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AbpCompanyName.AbpProjectName.Dto;
using AbpCompanyName.AbpProjectName.Entities;

namespace AbpCompanyName.AbpProjectName.Mapper
{
    internal static class CustomDtoMapper
    {
        //业务实体映射关系
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PdBrandEditDto, PdBrand>();
        }
    }
}
