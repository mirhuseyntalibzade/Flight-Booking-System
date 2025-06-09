using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.CountryDTOs
{
    public class GetIncludedCountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }
    }
}
