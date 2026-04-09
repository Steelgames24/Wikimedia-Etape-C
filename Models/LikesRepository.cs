using DAL;
using System.Linq;

namespace Models
{
    public class LikesRepository : Repository<Like>
    {
        public Like FindByUserAndMedia(int userId, int mediaId)
        {
            return ToList().FirstOrDefault(l => l.UserId == userId && l.MediaId == mediaId);
        }

        public void DeleteByMediaId(int mediaId)
        {
            var likesToDelete = ToList().Where(l => l.MediaId == mediaId).ToList();
            foreach (var like in likesToDelete)
            {
                Delete(like.Id);
            }
        }

        public void DeleteByUserId(int userId)
        {
            var likesToDelete = ToList().Where(l => l.UserId == userId).ToList();
            foreach (var like in likesToDelete)
            {
                Delete(like.Id);
            }
        }
    }
}