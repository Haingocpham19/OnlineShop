using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderViewdetailModel
    {
        public string productName { set; get; }
        public long ID { set; get; }
        public long ProductID { set; get; }
        public long OrderID { set; get; }
        public int? Quantity { set; get; }
        public decimal? Price { set; get; }
        public int? QuantityinStock { set; get; }
    }
}
