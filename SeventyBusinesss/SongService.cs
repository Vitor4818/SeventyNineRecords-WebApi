using Microsoft.EntityFrameworkCore;
using SeventyModel;
using SeventyData.Context;
using System.Collections.Generic;
using System.Linq;

namespace SeventyBusiness
{
    public class SongService
    {
        private readonly AppDbContext _context;

        public SongService(AppDbContext context)
        {
            _context = context;
        }

        public List<SongModel> ListarTodas()
        {
            return _context.Song.Include(s => s.Album).Include(s => s.Band).ToList();
        }

        public SongModel? ObterPorId(int id)
        {
            return _context.Song
                .Include(s => s.Album)
                .Include(s => s.Band)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<SongModel> ObterPorAlbumId(int albumId)
        {
            return _context.Song
                .Where(s => s.AlbumId == albumId)
                .Include(s => s.Album)
                .Include(s => s.Band)
                .ToList();
        }

        public SongModel Cadastrar(SongModel song)
        {
            _context.Song.Add(song);
            _context.SaveChanges();
            return song;
        }

        public bool Atualizar(SongModel song)
        {
            var existente = _context.Song.Find(song.Id);
            if (existente == null) return false;

            existente.Name = song.Name;
            existente.Duration = song.Duration;
            existente.AlbumId = song.AlbumId;
            existente.BandId = song.BandId;

            _context.Song.Update(existente);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var song = _context.Song.Find(id);
            if (song == null) return false;

            _context.Song.Remove(song);
            _context.SaveChanges();
            return true;
        }
    }
}
