using AutoMapper;
using BL.AdditionalServices;
using BL.DTOs.BlogDTOs;
using BL.DTOs.BlogDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using DAL.Repositories.Abstracts;
using QuestPDF.Elements;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Concretes
{
    public class BlogService : IBlogService
    {
        readonly IRepository<Blog> _repository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;

        public BlogService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IRepository<Blog> repository)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddBlogAsync(AddBlogDTO blogDTO)
        {
            Blog blog = _mapper.Map<Blog>(blogDTO);

            string folder = _webHostEnvironment.WebRootPath + "/uploads/bg/";
            blog.BackgroundImageURL = await ImageUpload.SaveFileAsync(blogDTO.BackgroundImage, folder);

            await _repository.AddAsync(blog);
        }

        public async Task<ICollection<GetBlogDTO>> GetAllBlogsAsync()
        {
            return _mapper.Map<ICollection<GetBlogDTO>>(await _repository.GetAllAsync());
        }

        public async Task<GetBlogDTO> GetBlogByIdAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            return _mapper.Map<GetBlogDTO>(blog);
        }
        public async Task RemoveBlogAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            _repository.Remove(blog);
        }

        public async Task RevertSoftDeleteBlog(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (!blog.isDeleted)
            {
                throw new BaseException("Item is already active.");
            }
            _repository.RevertSoftDelete(blog);
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await _repository.SaveChangesAsync();
            if (result == 0)
            {
                throw new BaseException("Couldn't save changes.");
            }
            return result;
        }

        public async Task SoftDeleteBlog(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (blog.isDeleted)
            {
                throw new BaseException("Item is already deleted.");
            }
            _repository.SoftDelete(blog);
        }

        public async Task UpdateBlogAsync(int id, UpdateBlogDTO blogDTO)
        {
            Blog oldBlog = await _repository.GetByIdAsync(id);
            if (oldBlog is null)
            {
                throw new BaseException("Couldn't find item.");
            }
            if (oldBlog.isDeleted)
            {
                throw new BaseException("You cannot update deleted item.");
            }
            Blog blog = _mapper.Map<Blog>(blogDTO);
            blog.Id = id;
            blog.CreatedDate = oldBlog.CreatedDate;
            blog.CreatedBy = oldBlog.CreatedBy;
            if (blogDTO.BackgroundImage is null)
            {
                blog.BackgroundImageURL = oldBlog.BackgroundImageURL;
            }
            else
            {
                string folder = _webHostEnvironment.WebRootPath + "/uploads/bg/";
                blog.BackgroundImageURL = await ImageUpload.SaveFileAsync(blogDTO.BackgroundImage, folder);
            }
            _repository.Update(blog);
        }
    }
}
