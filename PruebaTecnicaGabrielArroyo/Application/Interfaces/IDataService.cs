using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDataService
    {
        Task SaveDataFromApiAsync();
        Task<List<BanksEntities>> GetDataById(string id);
    }
}
