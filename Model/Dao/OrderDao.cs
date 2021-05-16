using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.ViewModel;

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
        public IEnumerable<OrderViewdetailModel> ViewDetail(long id)
        {
            var dao = new Product();
            var result = from a in db.Orders
                         join b in db.OrderDetails on a.ID equals b.OrderID
                         join c in db.Products on b.ProductID equals c.ID
                         where a.ID == id
                         select new OrderViewdetailModel()
                         {
                             productName = c.Name,
                             OrderID = a.ID,
                             ProductID = b.ProductID,
                             Quantity = b.Quantity,
                             Price = b.Price,
                             QuantityinStock = c.Quantity
                         };           
            return result.OrderBy(x => x.ProductID); 
        }
        public Order OrderDetail(long id)
        {
            return db.Orders.Find(id);         
        }
        
    }
}
