using CBS.Model.Dto;
using CBS.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CBS.Data.Entities;
using System.Linq;

namespace CBS.BLL.Services
{
    public class TimeslotService : Service<TimeSlot, TimeSlotDto>, ITimeslotService
    {
        private readonly IGenericRepository<TimeSlot> _repository;
        private readonly IMapper _mapper;

        public TimeslotService(IGenericRepository<TimeSlot> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimeSlotDto>> GetAvailableTimeslots(long clinicId)
        {
            var slots = await _repository.GetAll();
            var available = slots.Where(s => s.ClinicId == clinicId && s.IsTimeSlotOpen);
            return _mapper.Map<IEnumerable<TimeSlotDto>>(available);
        }

        public async Task<IEnumerable<TimeSlotDto>> GetAvailableTimeslotsByStaffId(long staffId)
        {
            var slots = await _repository.GetAll();
            var available = slots.Where(s => s.StaffId == staffId && s.IsTimeSlotOpen);
            return _mapper.Map<IEnumerable<TimeSlotDto>>(available);
        }
    }
}