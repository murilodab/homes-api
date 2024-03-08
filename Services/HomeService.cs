using homes_api.Data;
using homes_api.Models;
using homes_api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace homes_api.Services
{

    public class HomeService : IHomeService
    {

        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewHomeAsync(Home home)
        {
            try
            {
                if (home != null)
                {
                  

                    _context.Add(home);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Home>> GetAllHomesAsync()
        {
            try
            {
                List<Home> homes = await _context.Homes.Where(h => h.Available == true).ToListAsync();

                return homes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Home> GetHomeByIdAsync(int homeId)
        {
            try
            {
                Home home = await _context.Homes.FirstOrDefaultAsync(h => h.Id == homeId);
                
                return home;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task MakeHomeAvailableAsync(Home home)
        {
            try
            {
                home.Available = true;

                await UpdateHomeAsync(home.Id, home);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task MakeHomeUnavailableAsync(Home home)
        {
            try
            {
                home.Available = false;
                await UpdateHomeAsync(home.Id, home);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RemoveHomeAsync(int homeId)
        {
            try
            {
                Home home = await _context.Homes.FirstOrDefaultAsync(h => h.Id == homeId);

                if(home != null)
                {
                    _context.Homes.Remove(home);

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;



            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateHomeAsync(int id, Home newHome)
        {
            try
            {
                Home oldHome = _context.Homes.AsNoTracking().FirstOrDefault(h => h.Id == id);

                if(oldHome == null)
                {
                    return false;
                }

                oldHome.UpdatedDate = DateTime.Now.ToLocalTime();

                _context.Homes.Update(newHome);

                await _context.SaveChangesAsync();

                return true ;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
