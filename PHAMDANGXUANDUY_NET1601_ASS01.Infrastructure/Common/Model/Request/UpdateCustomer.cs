using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request
{
    public class UpdateCustomer
    {
        [Required]
        public string CustomerFullName { get; set; }
        [Required]

        public string Telephone { get; set; }
        //[Required]

        //public string EmailAddress { get; set; } = null!;
        //[Required]

        public DateTime? CustomerBirthday { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
