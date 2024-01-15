﻿using Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service
{
    public interface IBookingRevervationService
    {
        Task<List<ResponseBookingRevervation>> GetAll();
        Task<ResponseBookingRevervation> Add(CreateBookingReservation createBookingReservation);
        Task<ResponseBookingRevervation> GetById(int id);
        Task<List<ResponseBookingRevervation>> GetByCustomer(int customerId, DateTime dateTime);
        Task<ResponseBookingRevervation> UpdateCalculate(int id, UpdateBookingRevervation responseBookingRevervation);
        Task<List<ResponseBookingRevervation>> SearchDate(DateTime dateTime);
        Task<ResponseBookingRevervation> Delete(int id);

    }
}
