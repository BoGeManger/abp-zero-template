using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace AbpCompanyName.AbpProjectName.Dto
{
    public class PdBrandListDto:EntityDto<int>
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string LastModificationTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }

    public class PdBrandEditDto
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(64)]
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        [MaxLength(200)]
        /// <summary>
        /// 图标
        /// </summary>
        public string Logo { get; set; }
        [MaxLength(200)]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
