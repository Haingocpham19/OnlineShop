using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class OrderDao
    {
        OnlineShopDBContext db = null;
        public OrderDao()
        {
            db = new OnlineShopDBContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<OrderDetail> ViewDetail(long id)
        {
            var result = from a in db.Orders join b in db.OrderDetails
                         on a.ID equals b.OrderID
                         where a.ID == id
                         select new OrderDetail()
                         {                     
                             OrderID = a.ID,
                             ProductID = b.ProductID,
                             Quantity = b.Quantity,
                             Price = b.Price
                         };           
            return result.OrderBy(x => x.ProductID); ;
        }
    }
}
