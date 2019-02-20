using System;
using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Dto
{
    public class FileDto
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string FileToken { get; set; }

        public string Url { get; set; }

        public FileDto(string fileName, string fileType,string url)
        {
            Url = url;
            FileName = fileName;
            FileType = fileType;
            FileToken = Guid.NewGuid().ToString("N");
        }
    }
}
