using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace AbpCompanyName.AbpProjectName.Dto
{
    public class PdBrandGetPagedIn : PagedAndSortedInDto, IShouldNormalize
    {
        /// <summary>
        /// 模糊搜索使用的关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }

    public class PdBrandCreateOrUpdateIn
    {
        [Required]
        public PdBrandEditDto PdBrand { get; set; }
    }
}
