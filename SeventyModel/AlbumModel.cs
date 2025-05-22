using System.Text.Json.Serialization; 

namespace SeventyModel
{
    public class AlbumModel : MusicEntity
    {
        public int ReleaseYear { get; set; }
        public int BandId { get; set; }

        private readonly DateTime _createdAt = DateTime.Now;

        protected string AlbumCode => $"ALB-{Id}-{_createdAt.Year}";

        [JsonIgnore]
        public BandModel? Band { get; set; }

        public List<SongModel>? Songs { get; set; } = new();

        public string GetInternalCode()
        {
            return AlbumCode;
        }

        public override string GetDescription()
        {
            return $"Álbum: {Name} ({ReleaseYear})";
        }
    }
}
