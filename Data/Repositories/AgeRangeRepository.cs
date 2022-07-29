using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IstimAPI.Data.Repositories
{
    public class AgeRangeRepository : IAgeRangeRepository
    {
        private readonly DataContext _context;

        public AgeRangeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AgeRange>> GetAllAgeRangesAsync()
        {
            return await _context.AgeRanges
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AgeRange> GetAgeRangeByIdAsync(int ageRangeId)
        {
            return await _context.AgeRanges
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == ageRangeId);
        }

        public async Task CreateAgeRangeAsync(AgeRange ageRange)
        {
            _context.AgeRanges.Add(ageRange);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAgeRangeAsync(AgeRange ageRange)
        {
            _context.Entry(ageRange).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAgeRangeAsync(int ageRangeId)
        {
            var ageRange = await _context.AgeRanges.FirstOrDefaultAsync(x => x.Id == ageRangeId);
            _context.AgeRanges.Remove(ageRange);
            await _context.SaveChangesAsync();
        }

        public bool AgeRangeExists(int ageRangeId)
        {
            return _context.AgeRanges.Any(x => x.Id == ageRangeId);
        }
    }
}