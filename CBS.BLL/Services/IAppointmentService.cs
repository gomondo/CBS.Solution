using CBS.Data.Entities;
using CBS.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBS.BLL.Services
{
    public interface IAppointmentService:IService<Appointment,AppointmentDto>
    {

        Task<AppointmentDto> BookAppointment(long timeSlotId, AppointmentDto appointment);
        Task<bool> CancelAppointment(long appointmentId);
        Task<AppointmentDto>  ReschedulingAppointment(long appointmentId, AppointmentDto appointment);
    }
}
