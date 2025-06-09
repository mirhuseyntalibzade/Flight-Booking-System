using FluentValidation;
using Microsoft.AspNetCore.Http;
using BL.AdditionalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.BlogDTOs
{
    public class UpdateBlogDTO
    {
        public IFormFile BackgroundImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

    }

    public class UpdateBlogValidator : AbstractValidator<UpdateBlogDTO>
    {
        public UpdateBlogValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Blog title can not be empty.")
                .MaximumLength(100).WithMessage("Blog title can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Blog title can not be less than 3 characters.");

            RuleFor(c => c.Content)
                .NotEmpty().WithMessage("Blog content can not be empty.");

            RuleFor(c => c.Author)
                .NotEmpty().WithMessage("Blog author can not be empty.")
                .MaximumLength(100).WithMessage("Blog author can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Blog author can not be less than 3 characters.");

            RuleFor(c => c.ShortDesc)
                .NotEmpty().WithMessage("Blog short description can not be empty.");

            RuleFor(c => c.Category)
                .NotEmpty().WithMessage("Blog category can not be empty.")
                .MaximumLength(100).WithMessage("Blog category can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Blog category can not be less than 3 characters.");

            RuleFor(c => c.BackgroundImage)
                .Cascade(CascadeMode.Stop)
                .Must(a => a is null || a.Length < 5 * 1024 * 1024).WithMessage("Maximum file size is 10mb.")
                .Must(a => a is null || a.CheckType("image")).WithMessage("Only accepts image type");


        }
    }
}
