using OnTime.Application.Common.Interfaces;
using OnTime.Domain.User;
using OnTime.Infrastructure.Common.Persistence;

namespace OnTime.Infrastructure.Appointments.Persistence;

public class AppointmentsRepository(OnTimeDbContext _dbContext) : IAppointmentsRepository
{
    public async Task AddAsync(Appointment appointment, CancellationToken cancellation)
    {
        await _dbContext.AddAsync(appointment, cancellation);
        await _dbContext.SaveChangesAsync(cancellation);
    }

    public async Task<Appointment?> GetByIdAsync(Guid appointmentId, CancellationToken cancellation)
    {
        return await _dbContext.Appointments.FindAsync(appointmentId, cancellation);
    }
}
