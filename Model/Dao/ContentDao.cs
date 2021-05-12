using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDBContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDBContext();
        }
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public long Insert(Content content)
        {
            content.CreateDate = DateTime.Now;
            db.Contents.Add(content);
            db.SaveChanges();
            return content.ID;
        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.ModifiedDate = DateTime.Now;
                content.Name = entity.Name;
                content.Detail = entity.Detail;
                content.MetaTitle = entity.MetaTitle;
                content.Descriptions = entity.Descriptions;
                content.ModifiedBy = entity.ModifiedBy;
                content.Tags = entity.Tags;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Content GetById(long id)
        {
            return db.Contents.Find(id);
        }
        public List<Content> ListAllContent()
        {
            return db.Contents.OrderByDescending(x => x.CreateDate).ToList();
        }
        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }
    }
}
