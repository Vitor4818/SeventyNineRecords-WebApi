﻿using System.Text.Json.Serialization; 
namespace SeventyModel
{
    public class SongModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Duration { get; set; }
        public int AlbumId { get; set; }
        public int BandId { get; set; }


        [JsonIgnore]
        public AlbumModel? Album { get; set; }

        [JsonIgnore]
        public BandModel? Band { get; set; }
    }
}
