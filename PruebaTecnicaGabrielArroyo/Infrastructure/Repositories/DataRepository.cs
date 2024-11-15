using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly AppDbContext _context;

        public DataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveDataAsync(List<BanksEntities> entity)
        {
            try
            {
                _context.BanksEntities.ExecuteDelete();
                _context.BanksEntities.AddRange(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<List<BanksEntities>> GetDataByIdAsync(string bic)
        {
            var response = string.IsNullOrEmpty(bic) ? await _context.BanksEntities.ToListAsync() : [await _context.BanksEntities.FindAsync(bic)];

            return response;
        }
    }
}
