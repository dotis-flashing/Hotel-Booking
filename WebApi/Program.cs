using PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository;
using PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Mapper;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IUnitOfwork, Unitofwork>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IBookingDetailRepository, BookingDetailRepository>();
builder.Services.AddTransient<IBookingReservationRepository, BookingRevervationRepository>();
builder.Services.AddTransient<IBookingDetailSevice, BookingDetailService>();
builder.Services.AddTransient<IBookingRevervationService, BookingRevervationService>();
builder.Services.AddTransient<IRoomInforRepository, RoomInformationRepository>();
builder.Services.AddTransient<IRoomInforService, RoomInforService>();
builder.Services.AddTransient<IRoomTypeService, RoomTypeService>();
builder.Services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(ApplicationMapper).Assembly);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FUMiniHotelManagementContext>();

builder.Services.AddCors(c => c
            .AddDefaultPolicy(b => b
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseCors();
app.UseSession();
app.Run();
