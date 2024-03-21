using OnTime.Domain.Common;

namespace OnTime.Domain.User;

public class Appointment : Entity
{
    /// <summary>
    /// Title of appointment
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Date of appointment
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Location of appointment
    /// </summary>
    public string Location { get; set; }
    /// <summary>
    /// User that book appointment
    /// </summary>
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
}
