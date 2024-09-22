using RDLC_Report.Shared.Models;

namespace RDLC_Report.Shared.Services
{
    public class ReservationService:IReservationService
    {
        private readonly List<ReservationDetail> reservationDetails;

        public ReservationService()
        {
            reservationDetails = GenerateSampleData(50);
        }
        private List<ReservationDetail> GenerateSampleData(int count)
        {
            var data = new List<ReservationDetail>();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                data.Add(new ReservationDetail
                {
                    GuestName = $"Guest {i}",
                    Room = $"Room {random.Next(100, 200)}",
                    ArrivalDate = DateTime.Now.AddDays(random.Next(1, 30)),
                    DepartureDate = DateTime.Now.AddDays(random.Next(3, 10)),
                    ReservationStatus = i % 2 == 0 ? "Reserved" : "InHouse",
                    FlagStatus = GetRandomFlagStatus(random),
                    Topic = $"Topic {random.Next(1, 5)}",
                    Message = $"Message for reservation {i}",
                    DateEntered = DateTime.Now.AddDays(-random.Next(1, 10)),
                    EnteredBy = $"Staff {random.Next(1, 5)}",
                    DateDelivered = DateTime.Now.AddDays(-random.Next(1, 5)).AddHours(random.Next(1, 12)),
                    DeliveredBy = $"Staff {random.Next(1, 5)}",
                    Type = GetRandomType(random)
                });
            }
            return data;
        }
        private string GetRandomFlagStatus(Random random)
        {
            var flagStatuses = new[] { "Open", "Close", "Active", "None" };
            return flagStatuses[random.Next(flagStatuses.Length)];
        }
        private string GetRandomType(Random random)
        {
            var types = new[] { "AirlineInfo", "Alert", "BedBridge" };
            return types[random.Next(types.Length)];
        }
        public async Task<List<ReservationDetail>> GetAllReservations()
        {
            return reservationDetails;
        }
    }
}
