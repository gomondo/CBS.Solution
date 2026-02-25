using AutoMapper;
using CBS.Data.Entities;
using CBS.Data.Repository;
using CBS.Model.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBS.BLL.Services
{
    public class AppointmentService : Service<Appointment, AppointmentDto>, IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public AppointmentService(IGenericRepository<Appointment> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> BookAppointment(long timeSlotId, AppointmentDto appointment)
        {
            var entity = _mapper.Map<Appointment>(appointment);
            entity.TimeSlotId = timeSlotId;
            entity.IsActive = true;

            await _repository.Add(entity);
            return _mapper.Map<AppointmentDto>(entity);
        }

        public async Task<bool> CancelAppointment(long appointmentId)
        {
            var entity = await _repository.GetById(appointmentId);
            if (entity == null) return false;

            entity.IsActive = false;
            await _repository.Update(entity);
            return true;
        }

        public async Task<AppointmentDto> ReschedulingAppointment(long appointmentId, AppointmentDto appointment)
        {
            var entity = await _repository.GetById(appointmentId);
            if (entity == null) throw new Exception("Appointment not found");

            entity.TimeSlotId = appointment.TimeSlotId;
            entity.PatientId = appointment.PatientId;
            entity.IsActive = true;

            await _repository.Update(entity);
            return _mapper.Map<AppointmentDto>(entity);
        }
    }
}