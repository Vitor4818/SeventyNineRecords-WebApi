namespace SeventyModel
{
    public class BandModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public required int YearStarted { get; set; }
        public List<AlbumModel>? Albums { get; set; } = new();
    }
}