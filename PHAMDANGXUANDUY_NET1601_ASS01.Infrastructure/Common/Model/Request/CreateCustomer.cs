

using System.ComponentModel.DataAnnotations;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request
{
    public class CreateCustomer
    {
        [Required]
        public string CustomerFullName { get; set; }
        [Required]

        public string Telephone { get; set; }
        [Required]

        public string EmailAddress { get; set; } = null!;
        //[Required]

        public DateTime? CustomerBirthday { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
