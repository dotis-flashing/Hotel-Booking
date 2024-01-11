using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity
{
    public partial class Customer
    {
        public Customer()
        {
            BookingReservations = new HashSet<BookingReservation>();
        }

        public int CustomerId { get; set; }
        [JsonInclude]
        public string? CustomerFullName { get; set; }
        public string? Telephone { get; set; }
        public string EmailAddress { get; set; } = null!;
        public DateTime? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<BookingReservation> BookingReservations { get; set; }
    }
}
