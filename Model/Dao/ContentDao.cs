using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDBContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDBContext();
        }
        public long Insert(Content content)
        {
            db.Contents.Add(content);
            db.SaveChanges();
            return content.ID;
        }
        public Content GetById(long id)
        {
            return db.Contents.Find(id);
        }
        public List<Content> ListAllContent()
        {
            return db.Contents.OrderByDescending(x => x.CreateDate).ToList();
        }

     
    }
}
