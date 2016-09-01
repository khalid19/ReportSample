using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApplication.Model.CustomModel
{
   public class CustomerDetail
    {

       public string Name { get; set; }

       public string Email { get; set; }
       public string Title { get; set; }

       public string OrderNo { get; set; }

       public int Quantity { get; set; }


       public decimal UnitPrice { get; set; }


       public decimal TotatPrice { get; set; }


       public string City { get; set; }


    }
}
