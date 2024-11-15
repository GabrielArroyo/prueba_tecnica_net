using Application.Configuration;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Services
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataRepository;
        private readonly HttpClient _httpClient;
        private readonly ExternalConnection _externalConnection;

        public DataService(IDataRepository dataRepository, HttpClient httpClient, IOptions<ExternalConnection> externalConnection)
        {
            _dataRepository = dataRepository;
            _httpClient = httpClient;
            _externalConnection = externalConnection.Value;
        }

        public async Task SaveDataFromApiAsync()
        {
            var response = await _httpClient.GetAsync(_externalConnection.Url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al consumir la API externa");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<BanksEntities>>(content);

            if (data == null || !data.Any())
            {
                throw new Exception("No se obtuvieron datos válidos de la API");
            }

            await _dataRepository.SaveDataAsync(data);
        }
        public async Task<List<BanksEntities>> GetDataById(string? bic)
        {
            return await _dataRepository.GetDataByIdAsync(bic);
        }
    }
}
