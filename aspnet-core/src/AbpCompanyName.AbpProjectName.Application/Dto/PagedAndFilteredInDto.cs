using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace AbpCompanyName.AbpProjectName.Dto
{
    public class PagedAndFilteredInDto : IPagedResultRequest
    {
        [Range(1, 1000)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public string Filter { get; set; }

        public PagedAndFilteredInDto()
        {
            MaxResultCount = 10;
        }
    }
}
