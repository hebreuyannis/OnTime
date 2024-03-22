using MediatR;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Appointments.Queries.GetAppointment;

public class GetAppointmentQueryHandler(IAppointmentsRepository _appointmentsRepository) : IRequestHandler<GetAppointmentQuery, ErrorOr<Appointment>>
{
    public async Task<ErrorOr<Appointment>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        return await _appointmentsRepository.GetByIdAsync(request.appointmentId, cancellationToken) is Appointment appointment
            ? appointment
            : Error.NotFound(description:"Appointment not found");
    }
}
