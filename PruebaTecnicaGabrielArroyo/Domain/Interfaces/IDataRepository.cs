using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDataRepository
    {
        Task SaveDataAsync(List<BanksEntities> entity);
        Task<List<BanksEntities>> GetDataByIdAsync(string id);
    }
}
