using CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.PassengerDTOs
{
    public class AddPassengerDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string PassportNumber { get; set; }
    }
}
