using AutoMapper;
using BL.DTOs.CountryDTOs;
using CORE.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();
            CreateMap<Country, AddCountryDTO>().ReverseMap();
            CreateMap<Country, GetIncludedCountryDTO>().ReverseMap();
        }
    }
}
