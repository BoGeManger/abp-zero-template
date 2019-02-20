using Abp.Application.Services.Dto;

namespace AbpCompanyName.AbpProjectName.Dto
{
    public class PagedAndSortedInDto : PagedInDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndSortedInDto()
        {
            MaxResultCount = 10;
        }
    }
}
