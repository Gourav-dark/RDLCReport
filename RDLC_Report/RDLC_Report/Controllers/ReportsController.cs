using AspNetCore.Reporting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDLC_Report.Shared.Services;
using RDLC_Report.WebHelper;
using System.Data;
using System.Net.Mime;

namespace RDLC_Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportsController(IReservationService reservationService,IWebHostEnvironment webHostEnvironment) 
        {
            _reservationService = reservationService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("download")]
        public async Task<IActionResult> Get(DateTime? arrivalDateStart, DateTime? arrivalDateEnd, string? flagStatus)
        {

            var reservations = await _reservationService.GetAllReservations();

            //if (arrivalDateStart != null)
            //    reservations = reservations.Where(r => r.ArrivalDate >= arrivalDateStart.Value).ToList();

            //if (arrivalDateEnd != null)
            //    reservations = reservations.Where(r => r.ArrivalDate <= arrivalDateEnd.Value).ToList();

            //if (!string.IsNullOrEmpty(flagStatus) && flagStatus != "All")
            //    reservations = reservations.Where(r => r.FlagStatus == flagStatus).ToList();

            DataTable reservationDataTable = ConvertListToDataTable.ToDataTable(reservations.ToList());
            string wwwRootFolder = _webHostEnvironment.WebRootPath;
            string reportPath = Path.Combine(wwwRootFolder, @"Reports\ReservationRPT.rdlc");

            var localReport = new LocalReport(reportPath);

            localReport.AddDataSource("dsReservation", reservationDataTable);
            Dictionary<string,string> parameters=new Dictionary<string,string>();
            parameters.Add("ArrivalStartDate",arrivalDateStart?.ToString("MM/dd/yyyy"));
            parameters.Add("ArrivalEndDate", arrivalDateEnd?.ToString("MM/dd/yyyy"));
            if(flagStatus == null)
            {
                parameters.Add("FlagType", "All");
            }
            else
            {
                parameters.Add("FlagType", flagStatus);
            }
            //var reportResult = localReport.Execute(RenderType.Pdf, 1, null,"application/pdf");
            var reportResult = localReport.Execute(RenderType.Pdf, 1, parameters);
            return File(reportResult.MainStream, MediaTypeNames.Application.Octet, "Reservation.pdf");
            //return Ok(reservations);
            //return File(reportResult.MainStream, "application/pdf","ReservationRpt.pdf");
        }
    }
}
