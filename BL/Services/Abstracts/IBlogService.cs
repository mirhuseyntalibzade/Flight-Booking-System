using BL.DTOs.BlogDTOs;
using BL.DTOs.WrapperDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstracts
{
    public interface IBlogService
    {
        Task<ICollection<GetBlogDTO>> GetAllBlogsAsync();
        Task<GetBlogDTO> GetBlogByIdAsync(int id);
        Task AddBlogAsync(AddBlogDTO blogDTO);
        Task UpdateBlogAsync(int id, UpdateBlogDTO blogDTO);
        Task RemoveBlogAsync(int id);
        Task SoftDeleteBlog(int id);
        Task RevertSoftDeleteBlog(int id);
        Task<int> SaveChangesAsync();
    }
}
