using DAL;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class MediasRepository : Repository<Media>
    {
        public List<string> MediasCategories()
        {
            List<string> Categories = new List<string>();
            foreach (Media media in ToList().OrderBy(m => m.Category))
            {
                if (Categories.IndexOf(media.Category) == -1)
                {
                    Categories.Add(media.Category);
                }
            }
            return Categories;
        }

        public override bool Delete(int mediaId)
        {
            try
            {
                BeginTransaction();

                DB.Likes.DeleteByMediaId(mediaId);

                bool deleted = base.Delete(mediaId);

                EndTransaction();
                return deleted;
            }
            catch
            {
                EndTransaction();
                return false;
            }
        }
    }
}