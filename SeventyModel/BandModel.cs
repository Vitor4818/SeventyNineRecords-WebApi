namespace SeventyModel
{
    public class BandModel : MusicEntity
    {
        public required string Genre { get; set; }
        public int YearStarted { get; set; }
        public List<AlbumModel>? Albums { get; set; } = new();

        public override string GetDescription()
        {
            return $"Banda: {Name}, gênero: {Genre}, iniciou em: {YearStarted}";
        }
    }
}
