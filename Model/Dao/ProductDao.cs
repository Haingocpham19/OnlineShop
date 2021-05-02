using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace Model.Dao
{
    public class ProductDao
    {
        
        OnlineShopDBContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDBContext();
        }
        /// <summary>
        /// list lasted new products slide 1 - take3
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();

        }
        /// <summary>
        /// list lasted new products slide 2 - take3-skip3
        /// </summary>
        /// <returns></returns>
        public List<Product> ListNewProduct6()
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Skip(3).Take(3).ToList();

        }
        /// <summary>
        /// list all products on paging
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Quantity).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// list feature products by top hot
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureroduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null).OrderByDescending(x => x.CreateDate).Take(top).ToList();
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
        /// list sale off product
        /// </summary>
        /// <returns></returns>
        public List<Product> ListSaleOffProduct()
        {
            return db.Products.Where(x => x.PromotionPrice != null).Take(6).ToList();
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
