using Microsoft.EntityFrameworkCore;
using SeventyModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeventyData.Context;


namespace SeventyBusiness
{
    public class AlbumService
    {
        private readonly AppDbContext _context;

        public AlbumService(AppDbContext context)
        {
            _context = context;
        }

        public List<AlbumModel> ListarTodos()
        {
            return _context.Album.ToList();
        }

        public AlbumModel? ObterPorId(int id)
        {
            return _context.Album.Find(id);
        }

        public List<AlbumModel> ObterPorAno(int ano)
        {
            return _context.Album
                .Where(a => a.ReleaseYear == ano)
                .ToList();
        }

        public AlbumModel Cadastrar(AlbumModel album)
        {
            _context.Album.Add(album);
            _context.SaveChanges();
            return album;
        }

        public bool Atualizar(AlbumModel album)
        {
            var existente = _context.Album.Find(album.Id);
            if (existente == null) return false;

            existente.Name = album.Name;
            existente.ReleaseYear = album.ReleaseYear;
            existente.BandId = album.BandId;

            _context.Album.Update(existente);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var album = _context.Album.Find(id);
            if (album == null) return false;

            _context.Album.Remove(album);
            _context.SaveChanges();
            return true;
        }
    }
}
