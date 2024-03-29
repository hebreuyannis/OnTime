﻿using MediatR;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Appointments.Queries.GetAppointment
{
    public record GetAppointmentQuery(Guid appointmentId) : IRequest<ErrorOr<Appointment>>
    {
    }
}
