using Microsoft.EntityFrameworkCore;
using SeventyModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeventyData.Context;


namespace SeventyBusiness
{
    public class BandService
    {
        private readonly AppDbContext _context;

        public BandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BandModel>> GetAllAsync()
        {
            return await _context.Bands.Include(b => b.Albums).ToListAsync();
        }

        public async Task<BandModel?> GetByIdAsync(int id)
        {
            var band = await _context.Bands
                .Include(b => b.Albums)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (band != null)
            {
                Console.WriteLine($"[INFO] Banda encontrada: {band.GetDescription()}");
            }

            return band;
        }

        public async Task<List<BandModel>> SearchByNameAsync(string name)
        {
            return await _context.Bands
                .Include(b => b.Albums)
                .Where(b => b.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<BandModel> AddBandAsync(BandModel band)
        {
            _context.Bands.Add(band);
            await _context.SaveChangesAsync();
            return band;
        }

        public async Task<bool> UpdateAsync(BandModel band)
        {
            var existing = await _context.Bands
                .Include(b => b.Albums)
                .FirstOrDefaultAsync(b => b.Id == band.Id);

            if (existing == null) return false;

            existing.Name = band.Name;
            existing.Genre = band.Genre;
            existing.YearStarted = band.YearStarted;
            existing.Albums = band.Albums;

            _context.Bands.Update(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var band = await _context.Bands.FindAsync(id);
            if (band == null) return false;

            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
