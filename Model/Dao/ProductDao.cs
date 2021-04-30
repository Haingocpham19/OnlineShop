using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDBContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDBContext();
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();

        }
        public List<Product> ListFeatureroduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListRelatedroduct(long productID)
        {
            var product = db.Products.Find(productID);
            return db.Products.Where(x=>x.ID!=productID&&x.CategoryID==product.CategoryID).Take(4).ToList();
        }
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryID(long categoryID,ref int totalRecord, int pageIndex=1,int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.CreateDate).Count();
            var model = db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x=>x.CreateDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model;
        } 
        /// <summary>
        /// Get count products in category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public int getCount(long categoryID)
        {
            return db.Products.Where(x => x.CategoryID == categoryID).Count();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
