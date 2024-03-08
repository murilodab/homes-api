using homes_api.Models;

namespace homes_api.Services.Interfaces
{
    public interface IHomeService
    {
        public Task<List<Home>> GetAllHomesAsync();

        public Task<Home> GetHomeByIdAsync(int id);

        public Task<bool> AddNewHomeAsync(Home home);

        public Task MakeHomeUnavailableAsync(Home home);

        public Task MakeHomeAvailableAsync(Home home);

        public Task<bool> RemoveHomeAsync(int homeId);

        public Task<bool> UpdateHomeAsync (int id, Home home);



    }
}
