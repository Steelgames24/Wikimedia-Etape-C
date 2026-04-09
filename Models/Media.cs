using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public enum MediaSortBy { Title, PublishDate,Likes }

    public class Media : Record
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string YoutubeId { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public int OwnerId { get; set; } = 1;
        public bool Shared { get; set; } = true;
        [JsonIgnore]
        public User Owner => DB.Users.Get(OwnerId).Copy();

        public override bool IsValid()
        {
            if (!HasRequiredLength(Title, 1)) return false;
            if (!HasRequiredLength(Category, 1)) return false;
            if (!HasRequiredLength(Description, 1)) return false;
            if (DB.Medias.ToList().Where(m => m.YoutubeId == YoutubeId && m.Id != Id).Any()) return false;
            return true;
        }
        [JsonIgnore]
        public List<Like> Likes
        {
            get
            {
                return DB.Likes.ToList()
                    .Where(l => l.MediaId == Id)
                    .GroupBy(l => l.UserId)
                    .Select(g => g.First())
                    .ToList();
            }
        }

        [JsonIgnore]
        public int LikesCount
        {
            get
            {
                return Likes.Count;
            }
        }

        [JsonIgnore]
        public List<User> LikedUsers
        {
            get
            {
                return Likes
                    .Select(l => l.User)
                    .Where(u => u != null)
                    .GroupBy(u => u.Id)
                    .Select(g => g.First())
                    .ToList();
            }
        }

        [JsonIgnore]
        public bool LikedByConnectedUser
        {
            get
            {
                if (User.ConnectedUser == null) return false;
                return Likes.Any(l => l.UserId == User.ConnectedUser.Id);
            }
        }
    }
}