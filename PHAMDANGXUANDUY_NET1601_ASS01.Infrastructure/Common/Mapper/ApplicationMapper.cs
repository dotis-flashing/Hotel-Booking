using AutoMapper;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<CreateCustomer, Customer>()
                .ForMember(p => p.CustomerFullName, act => act.MapFrom(src => src.CustomerFullName))
                .ForMember(p => p.CustomerBirthday, act => act.MapFrom(src => src.CustomerBirthday))
                .ForMember(p => p.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
                .ForMember(p => p.Password, act => act.MapFrom(src => src.Password))
                .ForMember(p => p.Telephone, act => act.MapFrom(src => src.Telephone));

            CreateMap<UpdateCustomer, Customer>()
                .ForMember(p => p.CustomerFullName, act => act.MapFrom(src => src.CustomerFullName))
                .ForMember(p => p.CustomerBirthday, act => act.MapFrom(src => src.CustomerBirthday))
                //.ForMember(p => p.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
                .ForMember(p => p.Password, act => act.MapFrom(src => src.Password))
                .ForMember(p => p.Telephone, act => act.MapFrom(src => src.Telephone));
            CreateMap<Customer, ResponseCustomer>()
                .ForMember(p => p.CustomerId, act => act.MapFrom(src => src.CustomerId))
                .ForMember(p => p.CustomerFullName, act => act.MapFrom(src => src.CustomerFullName))
                .ForMember(p => p.CustomerBirthday, act => act.MapFrom(src => src.CustomerBirthday))
                .ForMember(p => p.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
                .ForMember(p => p.Password, act => act.MapFrom(src => src.Password))
                .ForMember(p => p.Telephone, act => act.MapFrom(src => src.Telephone))
                ;
            CreateMap<CreateRoomType, RoomType>()
                .ForMember(p => p.TypeNote, act => act.MapFrom(src => src.TypeNote))
                .ForMember(p => p.RoomTypeName, act => act.MapFrom(src => src.RoomTypeName))
                .ForMember(p => p.TypeDescription, act => act.MapFrom(src => src.TypeDescription));
            CreateMap<CreateBookingReservation, BookingReservation>()
               .ForMember(p => p.CustomerId, act => act.MapFrom(src => src.CustomerId))
               //.ForMember(p => p.TotalPrice, act => act.MapFrom(src => src.TotalPrice))
               .ForMember(dest => dest.BookingDetails, opt => opt.MapFrom(c => c.CreateBookingDetailList));
            CreateMap<CreateBookingDetail, BookingDetail>()
               .ForMember(p => p.RoomId, act => act.MapFrom(src => src.RoomId))
               .ForMember(p => p.StartDate, act => act.MapFrom(src => src.StartDate))
               .ForMember(p => p.EndDate, act => act.MapFrom(src => src.EndDate));
            //.ForMember(c => c.ActualPrice, act => act.MapFrom(c => c.ActualPrice));
            CreateMap<BookingDetail, ResponseBookingDetail>()
               .ForMember(p => p.RoomId, act => act.MapFrom(src => src.RoomId))
               .ForMember(p => p.BookingReservationId, act => act.MapFrom(src => src.BookingReservationId))
               .ForMember(p => p.StartDate, act => act.MapFrom(src => src.StartDate))
               .ForMember(p => p.EndDate, act => act.MapFrom(src => src.EndDate))
               .ForMember(c => c.ActualPrice, act => act.MapFrom(c => c.ActualPrice));
            CreateMap<CreateRoomInfor, RoomInformation>()
               .ForMember(p => p.RoomNumber, act => act.MapFrom(src => src.RoomNumber))
               .ForMember(p => p.RoomDetailDescription, act => act.MapFrom(src => src.RoomDetailDescription))
               .ForMember(p => p.RoomTypeId, act => act.MapFrom(src => src.RoomTypeId))
               .ForMember(p => p.RoomPricePerDay, act => act.MapFrom(src => src.RoomPricePerDay))
               .ForPath(c => c.RoomMaxCapacity, act => act.MapFrom(c => c.RoomMaxCapacity));
            CreateMap<BookingReservation, ResponseBookingRevervation>()
               .ForMember(p => p.CustomerId, act => act.MapFrom(src => src.CustomerId))
               .ForMember(p => p.TotalPrice, act => act.MapFrom(src => src.TotalPrice))
               .ForMember(p => p.BookingDetails, act => act.MapFrom(src => src.BookingDetails))
               .ForMember(p => p.BookingReservationId, act => act.MapFrom(src => src.BookingReservationId))
               .ForMember(p => p.BookingDate, act => act.MapFrom(src => src.BookingDate))
               .ForMember(p => p.TotalPrice, act => act.MapFrom(src => src.TotalPrice))
               .ForMember(p => p.BookingStatus, act => act.MapFrom(src => src.BookingStatus));

            CreateMap<RoomInformation, ResponseRoomInfor>()
              .ForMember(p => p.RoomTypeId, act => act.MapFrom(src => src.RoomTypeId))
              .ForMember(p => p.RoomNumber, act => act.MapFrom(src => src.RoomNumber))
              .ForMember(p => p.RoomPricePerDay, act => act.MapFrom(src => src.RoomPricePerDay))
              .ForMember(p => p.RoomStatus, act => act.MapFrom(src => src.RoomStatus))
              .ForMember(p => p.RoomMaxCapacity, act => act.MapFrom(src => src.RoomMaxCapacity))
              .ForMember(p => p.RoomNumber, act => act.MapFrom(src => src.RoomNumber))
              .ForMember(p => p.RoomId, act => act.MapFrom(src => src.RoomId));


            CreateMap<UpdateRoomInfor, RoomInformation>()
               .ForMember(p => p.RoomNumber, act => act.MapFrom(src => src.RoomNumber))
               .ForMember(p => p.RoomDetailDescription, act => act.MapFrom(src => src.RoomDetailDescription))
               .ForMember(p => p.RoomPricePerDay, act => act.MapFrom(src => src.RoomPricePerDay))
               .ForPath(c => c.RoomMaxCapacity, act => act.MapFrom(c => c.RoomMaxCapacity));
        }

    }
}
