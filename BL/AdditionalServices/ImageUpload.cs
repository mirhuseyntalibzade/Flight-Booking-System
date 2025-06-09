using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AdditionalServices
{
    public static class ImageUpload
    {
        public async static Task<string> SaveFileAsync(this IFormFile file,string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            string fileName = file.FileName;
            string path = destFolder + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            };
            return fileName;
        }

        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
    }
}
