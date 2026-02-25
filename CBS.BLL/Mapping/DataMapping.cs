using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CBS.Data.Entities;
using CBS.Model.Dto;
namespace CBS.BLL.Mapping
{
    public class DataMapping : Profile
    {
        public DataMapping()
        {

            CreateMap<TimeSlot, TimeSlotDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Clinic, ClinicDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();

        }
    }
}
