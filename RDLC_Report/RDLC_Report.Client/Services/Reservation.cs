using RDLC_Report.Shared.Models;

namespace RDLC_Report.Client.Services
{
    public class Reservation : IReservation
    {
        private readonly HttpClient _httpClient;
        public Reservation(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        public Task Download(string StartDate, string EndDate, string Flag)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReservationDetail>> Get(string StartDate, string EndDate, string Flag)
        {
            throw new NotImplementedException();
        }
    }
}
