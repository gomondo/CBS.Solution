
using Moq;
using Xunit;
using CBS.BLL.Services;
using CBS.Data.Entities;
using CBS.Data.Repository;
using CBS.Model.Dto;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace CBS.Tests
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IGenericRepository<Appointment>> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _mockRepo = new Mock<IGenericRepository<Appointment>>();
            _mockMapper = new Mock<IMapper>();
            _service = new AppointmentService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task BookAppointment_ShouldSetStatusToActive_AndSaveToRepo()
        {
            long timeSlotId = 5;
            var dto = new AppointmentDto { PatientId = 1 };
            var entity = new Appointment { PatientId = 1 };

            _mockMapper.Setup(m => m.Map<Appointment>(dto)).Returns(entity);
            _mockMapper.Setup(m => m.Map<AppointmentDto>(entity)).Returns(dto);

            var result = await _service.BookAppointment(timeSlotId, dto);

            Assert.True(entity.IsActive);
            Assert.Equal(timeSlotId, entity.TimeSlotId);
            _mockRepo.Verify(r => r.Add(entity), Times.Once);
        }

        [Fact]
        public async Task CancelAppointment_ShouldReturnFalse_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync((Appointment)null);
            var result = await _service.CancelAppointment(999);
            Assert.False(result);
        }
    }

    public class TimeslotServiceTests
    {
        private readonly Mock<IGenericRepository<TimeSlot>> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TimeslotService _service;

        public TimeslotServiceTests()
        {
            _mockRepo = new Mock<IGenericRepository<TimeSlot>>();
            _mockMapper = new Mock<IMapper>();
            _service = new TimeslotService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAvailableTimeslots_ShouldFilterCorrectly()
        {
            long targetClinicId = 1;
            var slots = new List<TimeSlot>
            {
                new TimeSlot { ClinicId = 1, IsTimeSlotOpen = true },
                new TimeSlot { ClinicId = 1, IsTimeSlotOpen = false }
            };

            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(slots);
            _mockMapper.Setup(m => m.Map<IEnumerable<TimeSlotDto>>(It.IsAny<IEnumerable<TimeSlot>>()))
                       .Returns(new List<TimeSlotDto> { new TimeSlotDto() });

            var result = await _service.GetAvailableTimeslots(targetClinicId);

            Assert.Single(result);
        }
    }
}
