

using System.ComponentModel.DataAnnotations;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response
{
    public class ResponseCustomer
    {
        public int CustomerId {  get; set; }
        public string CustomerFullName { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; } = null!;

        public DateTime? CustomerBirthday { get; set; }

        public string Password { get; set; }
    }
}
