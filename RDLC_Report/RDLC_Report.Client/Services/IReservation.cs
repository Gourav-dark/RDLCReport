using RDLC_Report.Shared.Models;

namespace RDLC_Report.Client.Services
{
    public interface IReservation
    {
        Task<List<ReservationDetail>> Get(string StartDate,string EndDate,string Flag);
        Task Download(string StartDate, string EndDate, string Flag);
    }
}
