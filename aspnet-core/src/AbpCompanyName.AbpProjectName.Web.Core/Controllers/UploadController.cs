using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using Abp.UI;
using AbpCompanyName.AbpProjectName.Dto;

namespace AbpCompanyName.AbpProjectName.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UploadController : AbpProjectNameControllerBase
    {
        private readonly IHostingEnvironment _environment;

        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<List<FileDto>> UploadImage()
        {
            var picList = new List<FileDto>();
            string path = string.Empty;
            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0) { throw new UserFriendlyException("请选择上传的文件"); }
            //格式限制
            var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };
            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    foreach (var file in files)
                    {
                        string strpath = Path.Combine("Upload", DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                        path = Path.Combine(_environment.WebRootPath, strpath);
                        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            await file.CopyToAsync(stream);
                        }
                        picList.Add(new FileDto(file.FileName, file.ContentType, strpath));
                    }
                }
                else
                {
                    throw new UserFriendlyException("图片过大");
                }
            }
            else
            {
                throw new UserFriendlyException("图片格式错误");
            }
            return picList;
        }
    }
}
