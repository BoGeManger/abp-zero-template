using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Entities
{
    public class PdBrand : FullAuditedEntity, IPassivable
    {
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

        public bool IsActive { get; set; }
    }
}
