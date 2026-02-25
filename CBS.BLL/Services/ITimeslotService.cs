using CBS.Data.Entities;
using CBS.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBS.BLL.Services
{
    public interface ITimeslotService:IService<TimeSlot,TimeSlotDto>
    {
        Task<IEnumerable<TimeSlotDto>> GetAvailableTimeslots(long clinicId);
        Task<IEnumerable<TimeSlotDto>> GetAvailableTimeslotsByStaffId(long stuffId);
    }
}
