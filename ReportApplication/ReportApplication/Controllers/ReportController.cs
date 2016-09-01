using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms;
using ReportApplication.DAL;
using ReportApplication.Model.CustomModel;
using ReportApplication.Model.Model;
using ReportApplication.Models.ViewModel;
using Warning = Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution.Warning;

namespace ReportApplication.Controllers
{
    public class ReportController : Controller
    {
        private PointOfSellDbContext db = new PointOfSellDbContext();
        //
        // GET: /Report/
        public ActionResult CustomerdDetail(DynamicReportHeaderViewModel model)
        {
            ModelState.Clear();
            return View(model);
        }

        public ActionResult CustomerdDetailByCity(string city, string fromDate, string toDate)
        {

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "CustomerDetails.rdlc");

            if (System.IO.File.Exists(path))
                lr.ReportPath = path;
            else
                return View("Index");
            Customer customer = db.Customers.FirstOrDefault(x => x.City == city) ?? new Customer();

            //List<Customer> customers = db.Customers.Where(x => x.City == city).ToList();

            //SqlParameter pC = new SqlParameter("@City", customers.Find(x => x.City == city));
            SqlParameter pC = new SqlParameter("@City", city);

            SqlParameter fromD = new SqlParameter("@FromDate", fromDate);
            SqlParameter toD = new SqlParameter("@ToDate", toDate);

            object[] objs = new object[] { pC, fromD, toD };

            List<CustomerDetail> customerDetails = new List<CustomerDetail>();
            customerDetails =
                db.Database.SqlQuery<CustomerDetail>("spGetCustomerOrderDetailByCity @City, @FromDate, @ToDate", objs[0], objs[1],
                    objs[2]).ToList();

            ReportDataSource rd = new ReportDataSource("PiontOfSell", customerDetails);

            lr.DataSources.Add(rd);
            string reportType = "pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + 2 + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11.7in</PageHeight>" +
                "  <MarginTop>0.4in</MarginTop>" +
                "  <MarginLeft>.4in</MarginLeft>" +
                "  <MarginRight>.2in</MarginRight>" +
                "  <MarginBottom>0.2in</MarginBottom>" +
                "</DeviceInfo>";

            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
    }
}