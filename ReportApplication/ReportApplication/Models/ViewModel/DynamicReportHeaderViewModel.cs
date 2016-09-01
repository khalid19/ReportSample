using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ReportApplication.Model.Model;

namespace ReportApplication.Models.ViewModel
{
    public class DynamicReportHeaderViewModel
    {


        public Order Order { get; set; }


        public List<Order> Orders { get; set; }

        public List<Item> Items { get; set; }

        public List<Customer> Customers { get; set; }

        public DynamicReportHeaderViewModel()
        {
            Order=new Order();
            Orders=new List<Order>();
            Items=new List<Item>();
            Customers=new List<Customer>();
        }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        public string City { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required")]
        public DateTime? ToDate { get; set; }


    }
}