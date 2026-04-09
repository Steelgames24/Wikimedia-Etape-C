using DAL;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Models
{
    public class Like : Record
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public User User
        {
            get
            {
                User user = DB.Users.Get(UserId);
                return user != null ? user.Copy() : null;
            }
        }

        [JsonIgnore]
        public Media Media
        {
            get
            {
                Media media = DB.Medias.Get(MediaId);
                return media != null ? media.Copy() : null;
            }
        }

        public override bool IsValid()
        {
            if (DB.Users.Get(UserId) == null) return false;
            if (DB.Medias.Get(MediaId) == null) return false;

            if (DB.Likes.ToList().Any(l => l.UserId == UserId && l.MediaId == MediaId && l.Id != Id))
                return false;

            return true;
        }
    }
}