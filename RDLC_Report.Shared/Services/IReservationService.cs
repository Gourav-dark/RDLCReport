using RDLC_Report.Shared.Models;

namespace RDLC_Report.Shared.Services
{
   
    public interface IReservationService
    {
        Task<List<ReservationDetail>> GetAllReservations();
    }
}
