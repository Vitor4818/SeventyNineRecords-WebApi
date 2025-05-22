namespace SeventyModel
{
    public abstract class MusicEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public abstract string GetDescription();
    }
}
