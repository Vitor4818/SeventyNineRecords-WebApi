using System.Text.Json.Serialization; 
namespace SeventyModel
{
    public class AlbumModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public int BandId { get; set; }


        [JsonIgnore]
        public BandModel? Band { get; set; }

        public List<SongModel>? Songs { get; set; } = new();
    }
}
