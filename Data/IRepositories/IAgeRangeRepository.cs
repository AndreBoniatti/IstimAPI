using System.Collections.Generic;
using System.Threading.Tasks;
using IstimAPI.Models;

namespace IstimAPI.Data.IRepositories
{
    public interface IAgeRangeRepository
    {
        Task<List<AgeRange>> GetAllAgeRangesAsync();
        Task<AgeRange> GetAgeRangeByIdAsync(int ageRangeId);
        Task CreateAgeRangeAsync(AgeRange ageRange);
        Task UpdateAgeRangeAsync(AgeRange ageRange);
        Task DeleteAgeRangeAsync(int ageRangeId);
        bool AgeRangeExists(int ageRangeId);
    }
}