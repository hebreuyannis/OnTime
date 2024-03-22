using OnTime.Domain.User;

namespace OnTime.Application.Common.Interfaces;
public interface IAppointmentsRepository
{
    Task AddAsync(Appointment appointment, CancellationToken cancellation);
    Task<Appointment?> GetByIdAsync(Guid appointmentId, CancellationToken cancellation);
}

